import type { Metadata } from "next";
import localFont from 'next/font/local';
import "./globals.css";

const baseNeueTrial = localFont({
  src: [
    {
      path: '../../public/fonts/BaseNeueTrial-ExpandedThin.ttf',
      weight: '100',
      style: 'normal',
    },
    {
      path: '../../public/fonts/BaseNeueTrial-ExpandedLight.ttf',
      weight: '300',
      style: 'normal',
    },
    {
      path: '../../public/fonts/BaseNeueTrial-Expanded.ttf',
      weight: '400',
      style: 'normal',
    },
    {
      path: '../../public/fonts/BaseNeueTrial-ExpandedMedium.ttf',
      weight: '500',
      style: 'normal',
    },
    {
      path: '../../public/fonts/BaseNeueTrial-ExpandedBold.ttf',
      weight: '700',
      style: 'normal',
    },
  ],
  variable: '--font-base-neue',
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
      <body className={baseNeueTrial.className}>
        {children}
      </body>
    </html>
  );
}
