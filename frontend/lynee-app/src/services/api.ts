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
  description: string;
  price: number;
  images: string[];
  category: string;
  brand: string;
  sizes: string[];
  colors: string[];
  inStock: boolean;
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

export const productsAPI = {
  getAll: async (params?: {
    category?: string;
    brand?: string;
    minPrice?: number;
    maxPrice?: number;
    search?: string;
    page?: number;
    limit?: number;
  }): Promise<{ products: Product[]; total: number; page: number; totalPages: number }> => {
    const response = await apiClient.get('/products', { params });
    return response.data;
  },

  getById: async (id: string): Promise<Product> => {
    const response = await apiClient.get(`/products/${id}`);
    return response.data;
  },

  getCategories: async (): Promise<string[]> => {
    const response = await apiClient.get('/products/categories');
    return response.data;
  },

  getBrands: async (): Promise<string[]> => {
    const response = await apiClient.get('/products/brands');
    return response.data;
  },
};

export const ordersAPI = {
  create: async (orderData: {
    items: { productId: string; quantity: number; size: string; color: string }[];
    shippingAddress: {
      street: string;
      city: string;
      state: string;
      zipCode: string;
      country: string;
    };
    paymentMethod: string;
  }) => {
    const response = await apiClient.post('/orders', orderData);
    return response.data;
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