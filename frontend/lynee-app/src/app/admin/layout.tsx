"use client";

import { usePathname, useRouter } from "next/navigation";
import { useAuthStore } from "@/stores/authStore";
import { useEffect } from "react";
import AdminSidebar from "@/components/admin/AdminSidebar";
import HeaderAdmin from "@/components/admin/HeaderAdmin";

export default function AdminLayout({ children }: { children: React.ReactNode }) {
  const pathname1 = usePathname();

   const pathname = usePathname();
  const router = useRouter();

  const { user, isAuthenticated, hasCheckedAuth, checkAuth } =
    useAuthStore();

  const isLoginPage = pathname === "/admin/denied";

  // 🔥 один раз перевіряємо auth
  useEffect(() => {
    checkAuth();
  }, [checkAuth]);

  // 🔐 guard ТІЛЬКИ для адмінки
  useEffect(() => {
    if (!hasCheckedAuth) return;
    if (isLoginPage) return;

    if (!isAuthenticated) {
      router.replace("/admin/login");
      return;
    }

    if (user?.role !== "Admin") {
      router.replace("/admin/denied");
    }
  }, [hasCheckedAuth, isAuthenticated, user, isLoginPage, router]);

  // ⏳ loading замість чорного екрану
  if (!hasCheckedAuth && !isLoginPage) {
    return <div className="p-8">Loading...</div>;
  }
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

