"use client";

import React from "react";
import Image from "next/image";
import cherryBag from "./image.png";

export default function AccountPage() {
  return (
    <div className="relative min-h-screen font-base m-0 p-0">
      <div className="absolute inset-0 -z-10">
        <Image
          src={cherryBag}
          alt="Cherry bag background"
          fill
          priority
          className="object-cover"
        />
      </div>

      <div className="flex min-h-screen">
        <div className="w-full md:w-1/2 p-8 md:p-16 bg-white/60 backdrop-blur-sm">
          <h1 className="text-black font-base font-normal text-[32px] leading-[100%] mb-6">
            Hello, Maria Shatanska!
          </h1>

          <div className="flex gap-6 text-black text-sm font-normal mb-8">
            <button className="hover:border-b hover:border-black pb-[2px]">
              My account
            </button>
            <button className="hover:border-b hover:border-black pb-[2px]">
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
    </div>
  );
}
