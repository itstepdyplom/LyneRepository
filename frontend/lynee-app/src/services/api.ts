import { OrderDto } from '@/utils/constants';
import apiClient from '../lib/axios';

export interface User {
  id: string;
  email: string;
  name: string;
  avatar?: string;
  addressId?: number;
  address?: Address;
}
export interface Address {
  id: number;
  street: string;
  city: string;
  state: string;
  zip: string;
  country: string;
}

export interface Product {
  id: string;
  name: string;
  description?: string;
  price: number;
  brand: string;
  categoryId: string;
  imageUrl: string;
  size?: string;
  color?: string;
  isActive: boolean;
}
export interface PagedResult<T> {
  items: T[];
  total: number;
  page: number;
  pageSize: number;
}

export interface LoginCredentials {
  email: string;
  password: string;
}

export interface RegisterData {
  name: string;
  forName: string;
  email: string;
  password: string;
  confirmPassword: string;
  gender?: string;
  dateOfBirth?: string;
  phoneNumber?: string;
}

export interface AuthResponse {
  token: string;
  email: string;
  name: string;
  forName: string;
  expiresAt: string;
}

export const authAPI = {
  login: async (credentials: LoginCredentials): Promise<AuthResponse> => {
    const response = await apiClient.post('/auth/login', credentials);
    return response.data;
  },

  register: async (data: RegisterData): Promise<AuthResponse> => {
    const response = await apiClient.post('/auth/register', data);
    return response.data;
  },

  logout: async (): Promise<void> => {
    await apiClient.post('/auth/logout');
  },

  me: async (): Promise<User> => {
    const response = await apiClient.get('/auth/user-info');
    return response.data;
  },
};
export interface PagedProductsResponse {
  products: Product[];
  total: number;
  page: number;
  totalPages: number;
}
export const productsAPI = {
  getAll: async (
  params?: {
    categoryName?: string;
    page?: number;
    limit?: number;
    brand?: string;
    minPrice?: number;
    maxPrice?: number;
    search?: string;
  },
  signal?: AbortSignal
): Promise<PagedResult<Product>> => {
  const response = await apiClient.get("/products", { params, signal });
  return response.data;
},

  getById: async (id: string): Promise<Product> => {
    const response = await apiClient.get(`/products/${id}`);
    return response.data;
  },

  getCategories: async (): Promise<string[]> => {
    const response = await apiClient.get('/categories');
    return response.data;
  },

  getBrands: async (): Promise<string[]> => {
    const response = await apiClient.get('/products/brands');
    return response.data;
  },
};

export type CreateOrderDto = {
  shippingAddressId: number;
  paymentMethod: string;
  items: { productId: string; quantity: number; unitPrice: number }[];
};

export const ordersAPI = {
  create: async (dto: CreateOrderDto) => {
    await apiClient.post("/orders", dto);
    return true;
  },

  getMyOrders: async (): Promise<OrderDto[]> => {
  const response = await apiClient.get('/orders/my');
  return response.data;
},

  getById: async (id: string) => {
    const response = await apiClient.get(`/orders/${id}`);
    return response.data;
  },
}; 