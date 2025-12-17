import { create } from "zustand";
import { Product, productsAPI, PagedResult, CreateProductDto } from "@/services/api";

interface ProductsState {
  products: Product[];
  loading: boolean;
  error: string | null;
  page: number;
  pageSize: number;
  total: number;

  fetchProducts: (params?: any) => Promise<void>;
  deleteProduct: (id: string) => Promise<void>;
  createProduct: (dto: CreateProductDto) => Promise<void>;
}

export const useProductsStore = create<ProductsState>((set) => ({
  products: [],
  loading: false,
  error: null,
  page: 1,
  pageSize: 20,
  total: 0,

  fetchProducts: async (params) => {
    set({ loading: true, error: null });
    try {
      const data: PagedResult<Product> = await productsAPI.getAll(params);
      set({
        products: data.items,
        total: data.total,
        page: data.page,
        pageSize: data.pageSize,
      });
    } catch (err: any) {
      set({ error: err.message || "Помилка при завантаженні продуктів" });
    } finally {
      set({ loading: false });
    }
  },
  deleteProduct: async (id: string) => {
    try {
      set({ loading: true, error: null });

      await productsAPI.delete(id);

      set((state) => ({
        products: state.products.filter((p) => p.id !== id),
      }));
    } catch (err: any) {
      set({ error: err.message || "Failed to delete product" });
    } finally {
      set({ loading: false });
    }
  },
 createProduct: async (dto) => {
    try {
      set({ loading: true });
      await productsAPI.create(dto);
    } catch (e: any) {
      set({ error: e.message || "Failed to create product" });
      throw e;
    } finally {
      set({ loading: false });
    }
  },
   
}));
