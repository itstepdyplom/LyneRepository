import type { Metadata } from "next";
import { Inter } from 'next/font/google';
import ThemeProvider from '../../components/providers/ThemeProvider';
import Header from '../../components/layout/Header';
import Footer from '../../components/layout/Footer';
import CartDrawer from '../../components/cart/CartDrawer';
import { NextIntlClientProvider } from 'next-intl';
import { getMessages } from 'next-intl/server';
import "../globals.css";

const inter = Inter({ 
  subsets: ["latin"],
  display: 'swap',
});

export const metadata: Metadata = {
  title: "LYNEE - Luxury Fashion",
  description: "Curating the finest luxury fashion from the world's most prestigious brands",
  keywords: "luxury fashion, designer clothes, premium brands, DIOR, PRADA, GUCCI",
  openGraph: {
    title: "LYNEE - Luxury Fashion",
    description: "Curating the finest luxury fashion from the world's most prestigious brands",
    type: "website",
  },
};

export default async function LocaleLayout({
  children,
  params,
}: {
  children: React.ReactNode;
  params: Promise<{ locale: string }>;
}) {
  const { locale } = await params;
  const messages = await getMessages({ locale });

  return (
    <NextIntlClientProvider messages={messages}>
      <ThemeProvider>
        <Header />
        <main style={{ minHeight: 'calc(100vh - 400px)' }}>
          {children}
        </main>
        <Footer />
        <CartDrawer />
      </ThemeProvider>
    </NextIntlClientProvider>
  );
} 