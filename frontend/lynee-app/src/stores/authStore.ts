import { create } from 'zustand';
import { authAPI, User, LoginCredentials, RegisterData } from '../services/api';

interface AuthState {
  user: User | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  error: string | null;
  
  login: (credentials: LoginCredentials) => Promise<void>;
  register: (data: RegisterData) => Promise<void>;
  logout: () => Promise<void>;
  checkAuth: () => Promise<void>;
  clearError: () => void;
}

export const useAuthStore = create<AuthState>((set, get) => ({
  user: null,
  isAuthenticated: false,
  isLoading: false,
  error: null,

  login: async (credentials: LoginCredentials) => {
    set({ isLoading: true, error: null });
    
    try {
      const { user, accessToken, refreshToken } = await authAPI.login(credentials);
      
      localStorage.setItem('accessToken', accessToken);
      localStorage.setItem('refreshToken', refreshToken);
      
      set({ 
        user, 
        isAuthenticated: true, 
        isLoading: false 
      });
    } catch (error: unknown) {
      let errorMessage = 'Login failed';
      if (typeof error === 'object' && error !== null && 'message' in error) {
        errorMessage = (error as { message?: string }).message || errorMessage;
      }
      set({ 
        error: errorMessage, 
        isLoading: false 
      });
      throw error;
    }
  },

  register: async (data: RegisterData) => {
    set({ isLoading: true, error: null });
    
    try {
      const { user, accessToken, refreshToken } = await authAPI.register(data);
      
      localStorage.setItem('accessToken', accessToken);
      localStorage.setItem('refreshToken', refreshToken);
      
      set({ 
        user, 
        isAuthenticated: true, 
        isLoading: false 
      });
    } catch (error: unknown) {
      let errorMessage = 'Registration failed';
      if (typeof error === 'object' && error !== null && 'message' in error) {
        errorMessage = (error as { message?: string }).message || errorMessage;
      }
      set({ 
        error: errorMessage, 
        isLoading: false 
      });
      throw error;
    }
  },

  logout: async () => {
    set({ isLoading: true });
    
    try {
      await authAPI.logout();
    } catch (error) {
      console.error('Logout error:', error);
    } finally {
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');
      
      set({ 
        user: null, 
        isAuthenticated: false, 
        isLoading: false,
        error: null
      });
    }
  },

  checkAuth: async () => {
    const token = localStorage.getItem('accessToken');
    
    if (!token) {
      set({ isAuthenticated: false, user: null });
      return;
    }

    set({ isLoading: true });
    
    try {
      const user = await authAPI.me();
      set({ 
        user, 
        isAuthenticated: true, 
        isLoading: false 
      });
    } catch (error) {
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');
      
      set({ 
        user: null, 
        isAuthenticated: false, 
        isLoading: false 
      });
    }
  },

  clearError: () => {
    set({ error: null });
  },
})); 