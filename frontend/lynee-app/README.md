# LYNEE - Luxury Fashion E-commerce

A modern luxury fashion e-commerce platform built with Next.js, Material-UI, Zustand, and Axios.

## 🏗️ Architecture Overview

This project follows a scalable and maintainable architecture with the following structure:

### Frontend Stack
- **Next.js 15** - React framework with App Router
- **Material-UI (MUI) v7** - Modern React UI framework
- **Zustand** - Lightweight state management
- **Axios** - HTTP client with interceptors
- **TypeScript** - Type safety and better DX

### Project Structure
```
src/
├── app/                    # Next.js App Router pages
├── components/
│   ├── layout/            # Header, Footer, Navigation
│   └── ui/                # Reusable UI components
├── hooks/                 # Custom React hooks
├── lib/                   # Core libraries (axios config)
├── services/              # API service functions
├── stores/                # Zustand state stores
├── theme/                 # MUI theme configuration
└── utils/                 # Utility functions and constants
```

## 🚀 Getting Started

### Prerequisites
- Node.js 18+ 
- npm or yarn

### Installation

1. **Install dependencies**
   ```bash
   npm install
   ```

2. **Set up environment variables**
   Create a `.env.local` file in the root directory:
   ```env
   NEXT_PUBLIC_API_URL=https://localhost:5050/api
   NEXT_PUBLIC_APP_URL=http://localhost:3000
   NEXT_PUBLIC_APP_NAME=LYNEE
   ```

3. **Run the development server**
   ```bash
   npm run dev
   ```

4. **Open your browser**
   Navigate to [http://localhost:3000](http://localhost:3000)

## 🏢 Features

### Core Functionality
- ✅ **Authentication System** - Login, register, logout with JWT tokens
- ✅ **Product Catalog** - Browse luxury fashion items by category
- ✅ **Shopping Cart** - Add, remove, update quantities with persistence
- ✅ **Responsive Design** - Mobile-first luxury UI/UX
- ✅ **State Management** - Global state with Zustand stores
- ✅ **API Integration** - Complete HTTP client with interceptors

### Architecture Features
- ✅ **Axios Interceptors** - Automatic token refresh and error handling
- ✅ **MUI Theme System** - Luxury brand styling with dark/light themes
- ✅ **TypeScript Integration** - Full type safety across the application
- ✅ **Component Library** - Reusable UI components
- ✅ **Custom Hooks** - Shared logic and localStorage management

## 🎨 Design System

### Color Palette
- **Primary**: Black (#000000) - Luxury and elegance
- **Secondary**: White (#FFFFFF) - Clean and minimal
- **Accent**: Gold/Beige (#D4A574) - Luxury touch
- **Text**: High contrast for readability

### Typography
- **Font Family**: Inter - Modern and clean
- **Hierarchy**: 6 heading levels with proper spacing
- **Letter Spacing**: Enhanced for luxury feel

### Components
All components follow MUI design principles with custom luxury styling:
- Sharp corners (borderRadius: 0) for premium feel
- Subtle animations and hover effects
- Consistent spacing using MUI's 8px grid system

## 🔧 API Integration

### Axios Configuration
The API client is pre-configured with:
- **Base URL** configuration
- **Request interceptors** for authentication tokens
- **Response interceptors** for error handling
- **Automatic token refresh** on 401 errors
- **Request/Response logging** in development

### API Services
All API calls are organized in service modules:
```typescript
// Example usage
import { authAPI, productsAPI } from '@/services/api';

// Authentication
await authAPI.login({ email, password });

// Products
const products = await productsAPI.getAll({ category: 'women' });
```

## 🗂️ State Management

### Zustand Stores

#### Auth Store (`useAuthStore`)
- User authentication state
- Login/logout functionality
- Token management
- User profile data

#### Cart Store (`useCartStore`)
- Shopping cart items
- Add/remove/update operations
- Persistent storage
- Total calculations

```typescript
// Example usage
import { useAuthStore, useCartStore } from '@/stores';

const { user, login, logout } = useAuthStore();
const { items, addItem, totalPrice } = useCartStore();
```

## 🎯 Environment Configuration

### Development
- Hot reload enabled
- Source maps for debugging
- Console logging for API calls
- React DevTools integration

### Production
- Optimized builds
- Error boundaries
- Performance monitoring ready
- SEO optimized

## 📱 Responsive Design

The application is fully responsive with breakpoints:
- **Mobile**: < 600px
- **Tablet**: 600px - 960px  
- **Desktop**: > 960px

All components adapt gracefully across devices with luxury UX maintained.

## 🔐 Security Features

- **JWT Token Management** - Secure authentication flow
- **Automatic Token Refresh** - Seamless user experience
- **Request Validation** - TypeScript interfaces for API data
- **Error Boundaries** - Graceful error handling
- **XSS Protection** - Sanitized user inputs

## 🚀 Deployment

### Build Commands
```bash
# Development
npm run dev

# Production build
npm run build

# Start production server
npm start

# Linting
npm run lint
```

### Environment Setup
Ensure all environment variables are properly configured for your deployment environment.

## 🤝 Contributing

1. Follow the established code structure
2. Use TypeScript for all new components
3. Follow MUI design system guidelines
4. Write responsive components
5. Add proper error handling
6. Include loading states

## 📄 License

This project is built for educational/portfolio purposes.

---

