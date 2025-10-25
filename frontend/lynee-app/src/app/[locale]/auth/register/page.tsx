"use client";

import React from "react";
import { useRouter } from "next/navigation";
import Image from "next/image";
import bgImage from "./woomanRegister.png";
import googleLogo from "./devicon_google.svg";

export default function RegisterPage() {
  const router = useRouter();

  return (
    <div className="flex min-h-screen font-base m-0 p-0">
      {/* Ліва картинка */}
      <div className="w-1/2 hidden md:flex items-center justify-center bg-[#f9f9f9] h-screen">
        <Image
          src={bgImage}
          alt="Model wearing white shirt standing on the beach"
          className="max-w-full max-h-full object-contain"
          priority
        />
      </div>

      {/* Права частина з формою */}
      <div className="w-full md:w-1/2 p-8 md:p-16 flex flex-col justify-center">
        <h1 className="text-black font-base font-normal text-[36px] leading-[100%] mb-6">
          Your profile
        </h1>

        {/* Tabs */}
        <div className="flex border-b border-black mb-6 text-black text-sm font-normal">
          <button
            className="pb-1 mr-6"
            onClick={() => router.push("/uk/auth/login")}
          >
            You are already a user
          </button>
          <button className="border-b-2 border-black pb-1">
            Create an account
          </button>
        </div>

        {/* Google button */}
        <button
          type="button"
          className="flex items-center gap-2 text-black text-sm font-normal mb-4"
        >
          <Image src={googleLogo} alt="Google G logo icon" width={20} height={20} />
          Log in with Google
        </button>

        <p className="text-xs text-black mb-4">or</p>

        {/* Register form */}
        <form className="space-y-4 text-xs text-gray-400 font-normal">
          <div>
            <label htmlFor="email" className="block mb-1">
              Email*
            </label>
            <input
              id="email"
              type="email"
              className="w-full bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
            />
          </div>

          <div>
            <label htmlFor="name" className="block mb-1">
              Your name*
            </label>
            <input
              id="name"
              type="text"
              className="w-full bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
            />
          </div>

          <div>
            <label className="block mb-1">Birthday</label>
            <div className="flex gap-2">
              <input
                type="text"
                placeholder="Day"
                className="w-1/3 bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
              />
              <input
                type="text"
                placeholder="Month"
                className="w-1/3 bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
              />
              <input
                type="text"
                placeholder="Year"
                className="w-1/3 bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
              />
            </div>
          </div>

          <div>
            <label htmlFor="country" className="block mb-1">
              Country*
            </label>
            <input
              id="country"
              type="text"
              className="w-full bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
            />
          </div>

          <div>
            <label htmlFor="password" className="block mb-1">
              Password*
            </label>
            <input
              id="password"
              type="password"
              className="w-full bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
            />
          </div>

          <div className="flex items-center mb-4 text-gray-300 text-xs">
            <input id="news" type="checkbox" className="mr-2" />
            <label htmlFor="news">Let me know about New in</label>
          </div>

          <button
            type="submit"
            className="w-full bg-black text-white text-xs py-2 rounded-sm font-normal"
          >
            Sign up
          </button>
        </form>
      </div>
    </div>
  );
}
