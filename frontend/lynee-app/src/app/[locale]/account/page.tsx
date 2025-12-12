"use client";

import React, { useEffect, useRef, useState } from "react";
import Image from "next/image";
import cherryBag from "./image.png";
import modelImage from "./imageMyOrders.png";
import redDress from "./reddressjpg.jpg";
import shorts from "./shorts.png";
import blueDress from "./dress.png";
import Contact from "./contactimg.jpg";
import { useAuthStore } from "@/stores/authStore";
import { useRouter } from "next/navigation";
import { ordersAPI } from "@/services/api"; // <-- перевір шлях
import { OrderDto } from "@/utils/constants";



export default function AccountOrdersContactPage() {
  const [activePage, setActivePage] = useState<"account" | "orders" | "contact">("account");
  const [showLogoutModal, setShowLogoutModal] = useState(false);

  const { user, isAuthenticated, isLoading, checkAuth, logout } = useAuthStore();
  const router = useRouter();

  // orders state
  const [orders, setOrders] = useState<OrderDto[]>([]);
  const [ordersLoading, setOrdersLoading] = useState(false);
  const [ordersError, setOrdersError] = useState<string | null>(null);

  const ran = useRef(false);

  useEffect(() => {
    if (ran.current) return;
    ran.current = true;
    checkAuth();
  }, [checkAuth]);

  useEffect(() => {
    if (!isLoading && !isAuthenticated) {
      router.push("/uk/auth/login");
    }
  }, [isLoading, isAuthenticated, router]);

  // fetch orders only when user opens "orders" tab and user is authenticated
  useEffect(() => {
    const load = async () => {
      if (activePage !== "orders") return;
      if (!isAuthenticated) return;

      setOrdersLoading(true);
      setOrdersError(null);

      try {
        const data = await ordersAPI.getMyOrders();
        setOrders(Array.isArray(data) ? data : []);
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      } catch (e: any) {
        setOrders([]);
        setOrdersError(e?.message ?? "Failed to load orders");
      } finally {
        setOrdersLoading(false);
      }
    };

    load();
  }, [activePage, isAuthenticated]);

  const LogoutModal = () => (
    <div className="fixed inset-0 flex items-center justify-center bg-black/50 backdrop-blur-sm z-50">
      <div className="bg-[#2E2E2E] p-8 rounded-md w-[90%] max-w-[420px] text-center">
        <p className="text-white text-xl mb-6">Are you sure you want to quit?</p>

        <button
          className="w-full bg-white text-black py-2 mb-4"
          onClick={async () => {
            await logout();
            router.push("/uk/auth/login");
          }}
        >
          Log out
        </button>

        <button
          className="w-full bg-gray-400 text-white py-2"
          onClick={() => setShowLogoutModal(false)}
        >
          Cancel
        </button>
      </div>
    </div>
  );

  const renderAccount = () => (
    <div className="flex min-h-screen font-base m-0 p-0">
      <div className="w-full md:w-1/2 p-8 md:p-16 bg-white/60 backdrop-blur-sm">
        <h1 className="text-black font-base font-normal text-[32px] leading-[100%] mb-6">
          Hello, {user?.name ?? "User"}!
        </h1>

        <div className="flex gap-6 text-black text-sm font-normal mb-8">
          <button
            className={`pb-[2px] ${activePage === "account" ? "border-b border-black" : "hover:border-b hover:border-black"}`}
            onClick={() => setActivePage("account")}
          >
            My account
          </button>

          <button
            className={`pb-[2px] ${activePage === "orders" ? "border-b border-black" : "hover:border-b hover:border-black"}`}
            onClick={() => setActivePage("orders")}
          >
            My orders
          </button>

          <button
            className={`pb-[2px] ${activePage === "contact" ? "border-b border-black" : "hover:border-b hover:border-black"}`}
            onClick={() => setActivePage("contact")}
          >
            Contact us
          </button>

          <button className="hover:border-b hover:border-black pb-[2px]" onClick={() => setShowLogoutModal(true)}>
            Log out
          </button>
        </div>

        <h2 className="text-black text-base font-medium mb-3">Personal information</h2>
        <div className="flex flex-col gap-4 w-full md:w-[85%] text-xs text-gray-400">
          <div>
            <label className="block mb-1">Email</label>
            <input
              type="email"
              className="w-full bg-white border border-gray-200 rounded-sm px-3 py-2 text-black"
              value={user?.email ?? ""}
              readOnly
            />
          </div>
          <div>
            <label className="block mb-1">Password</label>
            <input type="password" className="w-full bg-white border border-gray-200 rounded-sm px-3 py-2 text-black" />
          </div>
        </div>

        <h2 className="text-black text-base font-medium mt-8 mb-3 flex items-center gap-2">
          Your default delivery address
          <span className="text-[13px]" style={{ color: "rgba(0,0,0,0.5)" }}>(set a default)</span>
        </h2>

        <div className="flex flex-col gap-4 w-full md:w-[85%] text-xs">
          <div><label className="block mb-1 text-black/70">Country</label><input readOnly value={user?.address?.country ?? ""}  className="bg-white border border-gray-200 rounded-sm px-3 py-2 text-black" /></div>
          <div><label className="block mb-1 text-black/70">City</label><input readOnly value={user?.address?.city ?? ""}  className="bg-white border border-gray-200 rounded-sm px-3 py-2 text-black" /></div>
          <div><label className="block mb-1 text-black/70">Postal code</label><input readOnly value={user?.address?.zip ?? ""} className="bg-white border border-gray-200 rounded-sm px-3 py-2 text-black" /></div>
          <div><label className="block mb-1 text-black/70">Street</label><input readOnly value={user?.address?.street ?? ""}  className="bg-white border border-gray-200 rounded-sm px-3 py-2 text-black" /></div>
          <div><label className="block mb-1 text-black/70">House</label><input readOnly value={user?.address?.zip ?? ""}  className="bg-white border border-gray-200 rounded-sm px-3 py-2 text-black" /></div>
        </div>
      </div>

      <div className="hidden md:flex flex-col items-center w-1/2 pt-20">
        <h2 className="text-black text-base font-medium mb-4">
          Payment method
          <span className="text-[13px]" style={{ color: "rgba(0,0,0,0.5)" }}>(set a default)</span>
        </h2>

        <div className="flex gap-4 mb-8">
          <button className="px-4 py-1 border border-black text-xs hover:bg-gray-100 transition">ApplePay</button>
          <button className="px-4 py-1 border border-black text-xs hover:bg-gray-100 transition">MonoPay</button>
          <button className="px-4 py-1 border border-black text-xs hover:bg-gray-100 transition">By card</button>
        </div>
      </div>
    </div>
  );

  const renderOrders = () => (
    <div className="flex min-h-screen font-base m-0 p-0 bg-white">
      <div className="w-full md:w-1/2 p-8 md:p-16">
        <h1 className="text-black font-normal text-[28px] leading-[100%] mb-6">
          Hello, {user?.name ?? "User"}!
        </h1>

        <div className="flex gap-8 text-black text-sm font-normal mb-10">
          <button className="hover:border-b hover:border-black pb-1 transition" onClick={() => setActivePage("account")}>My account</button>
          <button className="border-b border-black pb-1 transition" onClick={() => setActivePage("orders")}>My orders</button>
          <button className="hover:border-b hover:border-black pb-1 transition" onClick={() => setActivePage("contact")}>Contact us</button>
          <button className="hover:border-b hover:border-black pb-1 transition" onClick={() => setShowLogoutModal(true)}>Log out</button>
        </div>

        {/* Orders states */}
        {ordersLoading && (
          <p className="text-black text-sm mb-6">Loading orders...</p>
        )}

        {!ordersLoading && ordersError && (
          <div className="mb-6">
            <p className="text-red-600 text-sm">Failed to load orders: {ordersError}</p>
            <button
              className="mt-3 bg-black text-white text-sm px-4 py-2 transition hover:opacity-80"
              onClick={() => {
                // trigger reload
                setActivePage("account");
                setTimeout(() => setActivePage("orders"), 0);
              }}
            >
              Try again
            </button>
          </div>
        )}

        {!ordersLoading && !ordersError && orders.length === 0 && (
          <div className="flex items-center gap-4 mb-10">
            <p className="text-black text-sm">OOPS, you don’t have any orders for now</p>
            <button className="bg-black text-white text-sm px-4 py-2 transition hover:opacity-80">
              Start now
            </button>
          </div>
        )}

        {!ordersLoading && !ordersError && orders.length > 0 && (
          <div className="space-y-4 mb-10">
            {orders.map((o) => (
              <div key={o.id} className="border border-gray-200 rounded-sm p-4">
                <div className="flex items-center justify-between">
                  <p className="text-black text-sm font-medium">Order #{o.id}</p>
                  <p className="text-black text-xs">
                    {new Date(o.date).toLocaleDateString()}
                  </p>
                </div>

                <div className="mt-2 text-xs text-gray-600 space-y-1">
                  <p><span className="text-black/70">Status:</span> {String(o.orderStatus)}</p>
                  <p><span className="text-black/70">Payment:</span> {o.paymentMethod || "-"}</p>
                  <p><span className="text-black/70">Tracking:</span> {o.trackingNumber || "-"}</p>
                  <p><span className="text-black/70">Items:</span> {o.productIds?.length ?? 0}</p>
                </div>
              </div>
            ))}
          </div>
        )}

        <div className="border-b border-gray-300 w-full mb-8"></div>

        <p className="text-black text-sm mb-6">We think you may like this</p>

        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
          <div className="text-center">
            <div className="bg-white border border-gray-200 flex justify-center items-center h-[260px]">
              <Image src={redDress} alt="Red overalls ZARA" width={140} height={240} />
            </div>
            <p className="text-black text-xs mt-3">Red overalls ZARA</p>
            <p className="text-black text-xs">1099 UAH</p>
          </div>
          <div className="text-center">
            <div className="bg-white border border-gray-200 flex justify-center items-center h-[260px]">
              <Image src={shorts} alt="Bermuda shorts MANGO" width={140} height={140} />
            </div>
            <p className="text-black text-xs mt-3">Bermuda shorts MANGO</p>
            <p className="text-black text-xs">2000 UAH</p>
          </div>
          <div className="text-center">
            <div className="bg-white border border-gray-200 flex justify-center items-center h-[260px]">
              <Image src={blueDress} alt="Draped denim dress Mohito" width={140} height={220} />
            </div>
            <p className="text-black text-xs mt-3">Draped denim dress Mohito</p>
            <p className="text-black text-xs">2099 UAH</p>
          </div>
        </div>
      </div>

      <div className="hidden md:block w-1/2 relative">
        <Image src={modelImage} alt="Model" fill className="object-cover" priority />
      </div>
    </div>
  );

  const renderContact = () => (
    <div className="flex min-h-screen font-base m-0 p-0">
      <div className="w-full md:w-1/2 p-8 md:p-16 bg-white/60 backdrop-blur-sm">
        <h1 className="text-black font-base font-normal text-[32px] leading-[100%] mb-6">
          Hello, {user?.name ?? "User"}!
        </h1>

        <div className="flex gap-6 text-black text-sm font-normal mb-8">
          <button className="hover:border-b hover:border-black pb-[2px]" onClick={() => setActivePage("account")}>My account</button>
          <button className="hover:border-b hover:border-black pb-[2px]" onClick={() => setActivePage("orders")}>My orders</button>
          <button className={`pb-[2px] ${activePage === "contact" ? "border-b border-black" : "hover:border-b hover:border-black"}`} onClick={() => setActivePage("contact")}>Contact us</button>
          <button className="hover:border-b hover:border-black pb-[2px]" onClick={() => setShowLogoutModal(true)}>Log out</button>
        </div>

        <p className="text-black text-sm mb-6">You have any questions? Contact us</p>

        <div className="flex flex-col gap-4 w-full md:w-[85%] text-xs">
          <input type="email" placeholder="Email*" className="w-full bg-gray-100 border border-gray-200 rounded-sm px-3 py-2 text-black" value={user?.email ?? ""} readOnly />
          <input type="text" placeholder="Your name*" className="w-full bg-gray-100 border border-gray-200 rounded-sm px-3 py-2 text-black" value={user?.name ?? ""} readOnly />
          <input type="tel" placeholder="Telephone number*" className="w-full bg-gray-100 border border-gray-200 rounded-sm px-3 py-2 text-black" />
          <select className="w-full bg-gray-100 border border-gray-200 rounded-sm px-3 py-2 text-black">
            <option>Choose the topic</option>
          </select>
          <textarea placeholder="Text" className="w-full bg-gray-100 border border-gray-200 rounded-sm px-3 py-2 text-black h-40"></textarea>
        </div>

        <button className="bg-black text-white text-sm px-4 py-2 mt-4 transition hover:opacity-80">Send an appeal</button>
      </div>

      <div className="hidden md:flex flex-col items-center w-1/2 p-16">
        <button className="bg-black text-white px-4 py-2 mb-6">Start a chat</button>
        <p className="text-black font-semibold">📞 0 (800) 313 234</p>
        <p className="text-black font-semibold">✉ lyne@gmail.com</p>
        <p className="text-black text-xs mt-4">
          Through the above channels, you can file a complaint or report incidents or safety issues related to the product to us.
        </p>
        <p className="text-gray-400 text-xs mt-2">
          Customer service center <br />
          Monday to Friday 9:00 a.m. to 5:00 p.m.
        </p>
      </div>
    </div>
  );

  const getBackgroundImage = () => {
    if (activePage === "account") return cherryBag;
    if (activePage === "contact") return Contact;
    return null;
  };

  const backgroundImage = getBackgroundImage();

  return (
    <div className="relative min-h-screen">
      {showLogoutModal && <LogoutModal />}

      {backgroundImage && (
        <div className="absolute inset-0 -z-10">
          <Image src={backgroundImage} alt="Background" fill className="object-cover" />
        </div>
      )}

      {activePage === "account" && renderAccount()}
      {activePage === "orders" && renderOrders()}
      {activePage === "contact" && renderContact()}
    </div>
  );
}