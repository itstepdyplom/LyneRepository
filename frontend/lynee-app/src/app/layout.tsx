import type { Metadata } from "next";
import { Inter } from 'next/font/google';
import "./globals.css";

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

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body className={inter.className}>
        {children}
      </body>
    </html>
  );
}
