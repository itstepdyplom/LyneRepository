import { create } from "zustand";
import { Category } from "@/services/api";
import { categoriesAPI } from "@/services/api";

interface CategoryState {
  categories: Category[];
  loading: boolean;
  error: string | null;

  fetchCategories: () => Promise<void>;
  deleteCategory: (id: string) => Promise<void>;
}

export const useCategoryStore = create<CategoryState>((set) => ({
  categories: [],
  loading: false,
  error: null,

  fetchCategories: async () => {
    set({ loading: true, error: null });

    try {
      const data = await categoriesAPI.getAll();
      set({ categories: data, loading: false });
    } catch (e: any) {
      set({
        error: e?.message || "Failed to load categories",
        loading: false,
      });
    }
  },
  deleteCategory: async (id: string) => {
      try {
        set({ loading: true, error: null });
  
        await categoriesAPI.delete(id);
  
        set((state) => ({
          categories: state.categories.filter((c) => c.id !== id),
        }));
      } catch (err: any) {
        set({ error: err.message || "Failed to delete category" });
      } finally {
        set({ loading: false });
      }
    },
}));
