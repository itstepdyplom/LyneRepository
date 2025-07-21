// API Endpoints
export const API_ENDPOINTS = {
  AUTH: {
    LOGIN: '/auth/login',
    REGISTER: '/auth/register',
    LOGOUT: '/auth/logout',
    ME: '/auth/me',
    REFRESH: '/auth/refresh',
  },
  PRODUCTS: {
    LIST: '/products',
    DETAIL: (id: string) => `/products/${id}`,
    CATEGORIES: '/products/categories',
    BRANDS: '/products/brands',
  },
  ORDERS: {
    CREATE: '/orders',
    LIST: '/orders/my',
    DETAIL: (id: string) => `/orders/${id}`,
  },
} as const;

// Local Storage Keys
export const STORAGE_KEYS = {
  ACCESS_TOKEN: 'accessToken',
  REFRESH_TOKEN: 'refreshToken',
  CART: 'cart-storage',
  THEME: 'theme-preference',
} as const;

// App Configuration
export const APP_CONFIG = {
  NAME: 'LYNEE',
  VERSION: '1.0.0',
  PAGINATION: {
    DEFAULT_PAGE_SIZE: 12,
    MAX_PAGE_SIZE: 100,
  },
  TIMEOUTS: {
    API_REQUEST: 10000, // 10 seconds
    DEBOUNCE_SEARCH: 300, // 300ms
  },
} as const;

// Product Categories
export const CATEGORIES = [
  'Women',
  'Men', 
  'Kids',
  'Accessories',
  'Sale',
] as const;

// Luxury Brands
export const BRANDS = [
  'DIOR',
  'PRADA', 
  'HERMÈS',
  'GUCCI',
  'Cartier',
  'CHANEL',
  'LOUIS VUITTON',
  'VERSACE',
  'ARMANI',
  'BALENCIAGA',
] as const;

// Product Sizes
export const SIZES = {
  CLOTHING: ['XS', 'S', 'M', 'L', 'XL', 'XXL'] as const,
  SHOES: ['35', '36', '37', '38', '39', '40', '41', '42', '43', '44', '45'] as const,
  ACCESSORIES: ['ONE SIZE'] as const,
};

// Colors
export const COLORS = [
  'Black',
  'White', 
  'Gray',
  'Navy',
  'Brown',
  'Beige',
  'Red',
  'Pink',
  'Blue',
  'Green',
  'Gold',
  'Silver',
] as const;

// Routes
export const ROUTES = {
  HOME: '/',
  WOMEN: '/women',
  MEN: '/men', 
  KIDS: '/kids',
  ACCESSORIES: '/accessories',
  SALE: '/sale',
  PRODUCT: (id: string) => `/product/${id}`,
  CART: '/cart',
  CHECKOUT: '/checkout',
  ACCOUNT: '/account',
  AUTH: {
    LOGIN: '/auth/login',
    REGISTER: '/auth/register',
    FORGOT_PASSWORD: '/auth/forgot-password',
  },
  ORDERS: '/orders',
  ORDER_DETAIL: (id: string) => `/orders/${id}`,
} as const; 