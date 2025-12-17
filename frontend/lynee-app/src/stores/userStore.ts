import { create } from "zustand";
import { usersAPI, UserListItem } from "@/services/api";

interface UsersState {
  users: UserListItem[];
  loading: boolean;
  error?: string;
  fetchUsers: () => Promise<void>;
  deleteUser: (id: string) => Promise<void>;
}

export const useUsersStore = create<UsersState>((set) => ({
  users: [],
  loading: false,
  error: undefined,

  fetchUsers: async () => {
    try {
      set({ loading: true });
      const users = await usersAPI.getAll();
      set({ users });
    } catch (e: any) {
      set({ error: e.message || "Failed to load users" });
    } finally {
      set({ loading: false });
    }
  },

  deleteUser: async (id) => {
    try {
      set({ loading: true });
      await usersAPI.delete(id);
      set((s) => ({ users: s.users.filter((u) => u.id !== id) }));
    } catch (e: any) {
      set({ error: e.message || "Failed to delete user" });
    } finally {
      set({ loading: false });
    }
  },
}));
