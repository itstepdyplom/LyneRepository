import { create } from "zustand";
import { authAPI, User, LoginCredentials, RegisterData } from "../services/api";
import axios from "axios";

interface AuthState {
  user: User | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  loadingAction: LoadingAction;
  error: string | null;

  hasCheckedAuth: boolean;          

  login: (credentials: LoginCredentials) => Promise<void>;
  register: (data: RegisterData) => Promise<void>;
  logout: () => Promise<void>;
  checkAuth: () => Promise<void>;
  clearError: () => void;
}
type LoadingAction = "checkAuth" | "login" | "register" | "logout" | null;

export const useAuthStore = create<AuthState>((set) => ({
  user: null,
  isAuthenticated: false,
  isLoading: false,
  loadingAction: null,
  error: null,
    
  hasCheckedAuth: false, 

  login: async (credentials: LoginCredentials) => {
    set({ isLoading: true, loadingAction: "login", error: null });

    try {
      const response = await authAPI.login(credentials);

      localStorage.setItem("accessToken", response.token);

      const user: User = {
        id: "",
        email: response.email,
        name: response.name,
      };

      set({
        user,
        isAuthenticated: true,
        isLoading: false,
        loadingAction: null,
      });
    } catch (error: unknown) {
      let errorMessage = "Login failed";
      if (typeof error === "object" && error !== null && "message" in error) {
        errorMessage = (error as { message?: string }).message || errorMessage;
      }
      set({
        error: errorMessage,
        isLoading: false,
        loadingAction: null,
      });
      throw error;
    }
  },

  register: async (data: RegisterData) => {
    set({ isLoading: true, loadingAction: "register", error: null });

    try {
      const response = await authAPI.register(data);

      localStorage.setItem("accessToken", response.token);

      const user: User = {
        id: "",
        email: response.email,
        name: response.name,
      };

      set({
        user,
        isAuthenticated: true,
        isLoading: false,
        loadingAction: null,
      });
    } catch (error: unknown) {
      let errorMessage = "Registration failed";
      if (typeof error === "object" && error !== null && "message" in error) {
        errorMessage = (error as { message?: string }).message || errorMessage;
      }
      set({
        error: errorMessage,
        isLoading: false,
        loadingAction: null,
      });
      throw error;
    }
  },

  logout: async () => {
    set({ isLoading: true, loadingAction: "logout" });

    try {
      await authAPI.logout();
    } catch (error) {
      console.error("Logout error:", error);
    } finally {
      localStorage.removeItem("accessToken");

      set({
        user: null,
        isAuthenticated: false,
        isLoading: false,
        loadingAction: null,
        error: null,
      });
    }
  },

  checkAuth: async () => {
    set({ loadingAction: "checkAuth" });
    const token = localStorage.getItem("accessToken");

    if (!token) {
      set({
        isAuthenticated: false,
        user: null,
        isLoading: false,
        loadingAction: null,
        hasCheckedAuth: true
      });
      return;
    }

    try {
      const user = await authAPI.me();
      set({
        user,
        isAuthenticated: true,
        isLoading: false,
        loadingAction: null,
        hasCheckedAuth: true
      });
    } catch (e) {
      const status = axios.isAxiosError(e) ? e.response?.status : undefined;

      if (status === 401 || status === 403) {
        localStorage.removeItem("accessToken");
        set({
          user: null,
          isAuthenticated: false,
          isLoading: false,
          loadingAction: null,
          hasCheckedAuth: true
        });
        return;
      }
      set({
        isLoading: false,
        loadingAction: null,
        hasCheckedAuth: true
      });
    }
  },

  clearError: () => {
    set({ error: null });
  },
}));
