"use client";

import React from "react";
import Image from "next/image";
import modelImage from "./imageMyOrders.png"; // заміниш на своє фото

export default function OrdersPage() {
  return (
    <div className="flex min-h-screen font-base m-0 p-0 bg-white">

      <div className="w-full md:w-1/2 p-8 md:p-16">

        <h1 className="text-black font-normal text-[28px] leading-[100%] mb-6">
          Hello, Maria Shatanska!
        </h1>

        <div className="flex gap-8 text-black text-sm font-normal mb-10">
          <button className="hover:border-b hover:border-black pb-1 transition">My account</button>
          <button className="hover:border-b hover:border-black pb-1 transition">My orders</button>
          <button className="hover:border-b hover:border-black pb-1 transition">Contact us</button>
          <button className="hover:border-b hover:border-black pb-1 transition">Log out</button>
        </div>

        <p className="text-black text-sm mb-6">
          OOPS, you don’t have any orders for now
        </p>

        <button className="bg-black text-white text-sm px-8 py-2 mb-10 transition hover:opacity-80">
          Start now
        </button>

        <div className="border-b border-gray-300 w-full mb-8"></div>

        <p className="text-black text-sm mb-6">We think you may like this</p>

        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">

          <div className="text-center">
            <div className="bg-white border border-gray-200 flex justify-center items-center h-[260px]">
              <Image src={"/red-dress.png"} alt="Item" width={140} height={240} />
            </div>
            <p className="text-black text-xs mt-3">Red overalls ZARA</p>
            <p className="text-black text-xs">1099 UAH</p>
          </div>

          <div className="text-center">
            <div className="bg-white border border-gray-200 flex justify-center items-center h-[260px]">
              <Image src={"/shorts.png"} alt="Item" width={140} height={140} />
            </div>
            <p className="text-black text-xs mt-3">Bermuda shorts MANGO</p>
            <p className="text-black text-xs">2000 UAH</p>
          </div>

          <div className="text-center">
            <div className="bg-white border border-gray-200 flex justify-center items-center h-[260px]">
              <Image src={"/blue-dress.png"} alt="Item" width={140} height={220} />
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
}
