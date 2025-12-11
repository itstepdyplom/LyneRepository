"use client";

import AdminSidebar from "@/components/admin/AdminSidebar";
import HeaderAdmin from "@/components/admin/HeaderAdmin";
import { usePathname } from "next/navigation";

export default function AdminLayout({ children }: { children: React.ReactNode }) {
  const pathname = usePathname();

  // Якщо сторінка логіну → без хедера і без сайдбару
  const isLoginPage = pathname === "/admin/login";

  return (
    <div style={{ display: "flex" }}>
      
      {/* SIDEBAR — показуємо тільки якщо це НЕ login */}
      {!isLoginPage && <AdminSidebar />}

      <div style={{ flex: 1, overflowY: "auto" }}>
        {/* HEADER — також тільки якщо НЕ login */}
        {!isLoginPage && <HeaderAdmin />}

        {children}
      </div>
    </div>
  );
}

