"use client";

import React, { useState } from "react";
import Image from "next/image";
import cherryBag from "./image.png";
import modelImage from "./imageMyOrders.png";
import redDress from "./reddressjpg.jpg";
import shorts from "./shorts.png";
import blueDress from "./dress.png";

export default function AccountAndOrdersPage() {
  const [activePage, setActivePage] = useState("account");

  const renderAccount = () => (
    <div className="flex min-h-screen font-base m-0 p-0">
      <div className="w-full md:w-1/2 p-8 md:p-16 bg-white/60 backdrop-blur-sm">
        <h1 className="text-black font-base font-normal text-[32px] leading-[100%] mb-6">
          Hello, Maria Shatanska!
        </h1>

        <div className="flex gap-6 text-black text-sm font-normal mb-8">
          <button
            className="hover:border-b hover:border-black pb-[2px]"
            onClick={() => setActivePage("account")}
          >
            My account
          </button>
          <button
            className="hover:border-b hover:border-black pb-[2px]"
            onClick={() => setActivePage("orders")}
          >
            My orders
          </button>
          <button className="hover:border-b hover:border-black pb-[2px]">
            Contact us
          </button>
          <button className="hover:border-b hover:border-black pb-[2px]">
            Log out
          </button>
        </div>

        <h2 className="text-black text-base font-medium mb-3">
          Personal information
        </h2>

        <div className="flex flex-col gap-4 w-full md:w-[85%] text-xs text-gray-400">
          <div>
            <label className="block mb-1">Email</label>
            <input
              type="email"
              value="mariashatanska@gmail.com"
              className="w-full bg-white border border-gray-200 rounded-sm px-3 py-2 text-black"
            />
          </div>

          <div>
            <label className="block mb-1">Password</label>
            <input
              type="password"
              value="************"
              className="w-full bg-white border border-gray-200 rounded-sm px-3 py-2 text-black"
            />
          </div>
        </div>

        <h2 className="text-black text-base font-medium mt-8 mb-3 flex items-center gap-2">
          Your default delivery address
          <span className="text-[13px]" style={{ color: "rgba(0,0,0,0.5)" }}>
            (set a default)
          </span>
        </h2>

        <div className="flex flex-col gap-4 w-full md:w-[85%] text-xs">
          <div>
            <label className="block mb-1 text-black/70">Country</label>
            <input className="bg-white border border-gray-200 rounded-sm px-3 py-2 text-black" />
          </div>
          <div>
            <label className="block mb-1 text-black/70">City</label>
            <input className="bg-white border border-gray-200 rounded-sm px-3 py-2 text-black" />
          </div>
          <div>
            <label className="block mb-1 text-black/70">Postal code</label>
            <input className="bg-white border border-gray-200 rounded-sm px-3 py-2 text-black" />
          </div>
          <div>
            <label className="block mb-1 text-black/70">Street</label>
            <input className="bg-white border border-gray-200 rounded-sm px-3 py-2 text-black" />
          </div>
          <div>
            <label className="block mb-1 text-black/70">House</label>
            <input className="bg-white border border-gray-200 rounded-sm px-3 py-2 text-black" />
          </div>
        </div>
      </div>

      <div className="hidden md:flex flex-col items-center w-1/2 pt-20">
        <h2 className="text-black text-base font-medium mb-4">
          Payment method
          <span className="text-[13px]" style={{ color: "rgba(0,0,0,0.5)" }}>
            (set a default)
          </span>
        </h2>

        <div className="flex gap-4 mb-8">
          <button className="px-4 py-1 border border-black text-xs hover:bg-gray-100 transition">
            ApplePay
          </button>
          <button className="px-4 py-1 border border-black text-xs hover:bg-gray-100 transition">
            MonoPay
          </button>
          <button className="px-4 py-1 border border-black text-xs hover:bg-gray-100 transition">
            By card
          </button>
        </div>
      </div>
    </div>
  );

  const renderOrders = () => (
    <div className="flex min-h-screen font-base m-0 p-0 bg-white">
      <div className="w-full md:w-1/2 p-8 md:p-16">
        <h1 className="text-black font-normal text-[28px] leading-[100%] mb-6">
          Hello, Maria Shatanska!
        </h1>

        <div className="flex gap-8 text-black text-sm font-normal mb-10">
          <button
            className="hover:border-b hover:border-black pb-1 transition"
            onClick={() => setActivePage("account")}
          >
            My account
          </button>
          <button
            className="hover:border-b hover:border-black pb-1 transition"
            onClick={() => setActivePage("orders")}
          >
            My orders
          </button>
          <button className="hover:border-b hover:border-black pb-1 transition">
            Contact us
          </button>
          <button className="hover:border-b hover:border-black pb-1 transition">
            Log out
          </button>
        </div>

       <div className="flex items-center gap-4 mb-10">
  <p className="text-black text-sm">
    OOPS, you don’t have any orders for now
  </p>
  <button className="bg-black text-white text-sm px-4 py-2 transition hover:opacity-80">
    Start now
  </button>
</div>


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
        <Image
          src={modelImage}
          alt="Model"
          fill
          className="object-cover"
          priority
        />
      </div>
    </div>
  );

  return (
    <div className="relative min-h-screen">
      <div className="absolute inset-0 -z-10">
        <Image src={cherryBag} alt="Background" fill className="object-cover" />
      </div>

      {activePage === "account" ? renderAccount() : renderOrders()}
    </div>
  );
}
