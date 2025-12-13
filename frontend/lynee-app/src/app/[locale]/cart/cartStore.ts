import { create } from 'zustand';
import { persist } from 'zustand/middleware';

export interface CartItem {
  id: string;
  productId: string;
  name: string;
  price: number;
  image: string;
  size: string;
  color: string;
  quantity: number;
}

interface CartState {
  items: CartItem[];
  isOpen: boolean;

  addItem: (item: Omit<CartItem, 'id' | 'quantity'> & { quantity?: number }) => void;
  removeItem: (id: string) => void;
  updateQuantity: (id: string, quantity: number) => void;
  clearCart: () => void;
  openCart: () => void;
  closeCart: () => void;
}

export const useCartStore = create<CartState>()(
  persist(
    (set, get) => ({
      items: [],
      isOpen: false,

      addItem: (newItem) => {
        const { items } = get();

        const existingItemIndex = items.findIndex(
          (item) =>
            item.productId === newItem.productId &&
            item.size === newItem.size &&
            item.color === newItem.color
        );

        if (existingItemIndex >= 0) {
          const updatedItems = [...items];
          updatedItems[existingItemIndex].quantity += newItem.quantity || 1;
          set({ items: updatedItems });
        } else {
          const cartItem: CartItem = {
            ...newItem,
            id: `${newItem.productId}-${newItem.size}-${newItem.color}-${Date.now()}`,
            quantity: newItem.quantity || 1,
          };
          set({ items: [...items, cartItem] });
        }
      },

      removeItem: (id) => set({ items: get().items.filter((i) => i.id !== id) }),

      updateQuantity: (id, quantity) => {
        if (quantity <= 0) return get().removeItem(id);
        set({
          items: get().items.map((i) => (i.id === id ? { ...i, quantity } : i)),
        });
      },

      clearCart: () => set({ items: [] }),
      openCart: () => set({ isOpen: true }),
      closeCart: () => set({ isOpen: false }),
    }),
    {
      name: 'cart-storage',
      partialize: (state) => ({ items: state.items }),
    }
  )
);
