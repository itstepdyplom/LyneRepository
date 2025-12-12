export interface Category {
  key: string;        
  href: string;       
  hasSubmenu?: boolean;
  subCategories?: Category[];
}

export const getCategoryHref = (locale: string, categoryKey: string): string => {
  const routeMap: Record<string, string> = {
    'women': `/categories/women`,
    'men': `/categories/men`,
    'kids': `/categories/kids`,
    'accessories': `/categories/accessories`,
    'bagsAndWallets': `/categories/bags`,
    'viewAll': ``,
    'new': ``,
    'home': `/categories/home`,
    'account' : '/frontend/lynee-app/src/app/[locale]/account'
  };
  const route = routeMap[categoryKey] || ``;
  return route ? `/${locale}${route}` : ``;
};

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