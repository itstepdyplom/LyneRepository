export interface Category {
  key: string;        
  href: string;       
  hasSubmenu?: boolean;
  subCategories?: Category[];
}

export const categories: Category[] = [
  { key: 'viewAll', href: `` },
  { key: 'new', href: `` },
  { key: 'women', href: ``, hasSubmenu: true },
  { key: 'men', href: ``, hasSubmenu: true },
  { key: 'kids', href: ``, hasSubmenu: true },
  { key: 'bagsAndWallets', href: ``, hasSubmenu: true },
  { key: 'accessories', href: ``, hasSubmenu: true },
  { key: 'home', href: ``, hasSubmenu: true },
];