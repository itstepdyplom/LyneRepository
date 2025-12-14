"use client";

import React, { useState } from "react";
import { useRouter } from "next/navigation";
import Image from "next/image";
import bgImage from "./woomanLogin.png";
import googleLogo from "./devicon_google.svg";
import { useAuthStore } from "@/stores/authStore";

export default function LoginPage() {
  const router = useRouter();
  const { login, isLoading, loadingAction, error, clearError } = useAuthStore();
  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });

  return (
    <div className="flex min-h-screen font-base m-0 p-0">
      {/* Ліва картинка */}
      <div className="w-1/2 hidden md:block relative h-screen">
        <Image
          src={bgImage}
          alt="Model in blue denim dress with sunglasses standing on beige background"
          fill
          className="object-cover"
          priority
        />
      </div>

      {/* Права частина з формою */}
      <div className="w-full md:w-1/2 p-8 md:p-16">
        <h1 className="text-black font-base font-normal text-[36px] leading-[100%] mb-6">
          Your profile
        </h1>

        {/* Tabs */}
        <div className="flex border-b border-black mb-6 text-black text-sm font-normal">
          <button className="border-b-2 border-black pb-1 mr-6 transition-colors duration-200 hover:text-gray-600 hover:border-gray-600">
            I am already a user
          </button>
          <button 
            className="pb-1 transition-colors duration-200 hover:text-gray-600 border-b-2 border-transparent hover:border-gray-600"
            onClick={() => router.push("/uk/auth/register")}
          >
            Create an account
          </button>
        </div>

        {/* Google button */}
        <button
          type="button"
          className="flex items-center gap-2 text-black text-sm font-normal mb-4 transition-colors duration-200 hover:text-gray-600 w-fit border-b border-transparent hover:border-gray-600 pb-1"
        >
          <Image src={googleLogo} alt="Google G logo icon" width={20} height={20} />
          Log in with Google
        </button>

        <p className="text-xs text-black mb-4">or</p>

        {/* Login form */}
        <form 
          className="space-y-4 text-xs text-gray-400 font-normal"
          onSubmit={async (e) => {
            e.preventDefault();
            clearError();
            try {
              await login(formData);
              router.push("/uk");
            } catch (err) {
              console.error("Login error:", err);
            }
          }}
        >
          {error && (
            <div className="text-red-500 text-xs mb-2">{error}</div>
          )}
          
          <div>
            <label htmlFor="email" className="block mb-1">
              Email
            </label>
            <input
              id="email"
              type="email"
              required
              value={formData.email}
              onChange={(e) => setFormData({ ...formData, email: e.target.value })}
              className="w-full bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
            />
          </div>

          <div>
            <label htmlFor="password" className="block mb-1">
              Password
            </label>
            <input
              id="password"
              type="password"
              required
              value={formData.password}
              onChange={(e) => setFormData({ ...formData, password: e.target.value })}
              className="w-full bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
            />
          </div>

          <div className="text-xs text-gray-300 mb-2 cursor-pointer transition-colors duration-200 hover:text-gray-600 w-fit">
            Forgot a password?
          </div>

          <div className="flex items-center mb-6 text-gray-300 text-xs">
            <input id="remember" type="checkbox" className="mr-2" />
            <label htmlFor="remember">Remember me</label>
          </div>

          <button
            type="submit"
            disabled={loadingAction==='login'}
            className="w-full bg-black text-white text-xs py-2 rounded-sm font-normal disabled:opacity-50 disabled:cursor-not-allowed"
          >
            {loadingAction === "login" ? "Logging in..." : "Log in"}
          </button>
        </form>
      </div>
    </div>
  );
}