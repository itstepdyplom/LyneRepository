"use client";

import React, { useState } from "react";
import { useRouter } from "next/navigation";
import Image from "next/image";
import bgImage from "./woomanRegister.png";
import googleLogo from "./devicon_google.svg";
import { useAuthStore } from "@/stores/authStore";

export default function RegisterPage() {
  const router = useRouter();
  const { register, isLoading, error, clearError } = useAuthStore();
  const [formData, setFormData] = useState({
    name: "",
    forName: "",
    email: "",
    password: "",
    confirmPassword: "",
    gender: "",
    dateOfBirth: "",
    phoneNumber: "",
  });
  const [validationError, setValidationError] = useState("");

  return (
    <div className="flex min-h-screen font-base m-0 p-0">
      {/* Ліва картинка */}
      <div className="w-1/2 hidden md:block h-screen overflow-hidden">
        <Image
          src={bgImage}
          alt="Model wearing white shirt standing on the beach"
          className="w-full h-full object-cover object-center"
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
            className="pb-1 mr-6 transition-colors duration-200 hover:text-gray-600 border-b-2 border-transparent hover:border-gray-600"
            onClick={() => router.push("/uk/auth/login")}
          >
            You are already a user
          </button>
          <button className="border-b-2 border-black pb-1 transition-colors duration-200 hover:text-gray-600 hover:border-gray-600">
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

        {/* Register form */}
        <form 
          className="space-y-4 text-xs text-gray-400 font-normal"
          onSubmit={async (e) => {
            e.preventDefault();
            clearError();
            setValidationError("");

            if (formData.password !== formData.confirmPassword) {
              setValidationError("Passwords do not match");
              return;
            }

            if (formData.password.length < 6) {
              setValidationError("Password must be at least 6 characters");
              return;
            }

            try {
              const registerData: any = {
                name: formData.name,
                forName: formData.forName,
                email: formData.email,
                password: formData.password,
                confirmPassword: formData.confirmPassword,
              };

              if (formData.gender) {
                registerData.gender = formData.gender;
              }

              if (formData.dateOfBirth) {
                const date = new Date(formData.dateOfBirth);
                const year = date.getFullYear();
                const month = String(date.getMonth() + 1).padStart(2, '0');
                const day = String(date.getDate()).padStart(2, '0');
                registerData.dateOfBirth = `${year}-${month}-${day}`;
              }

              if (formData.phoneNumber) {
                registerData.phoneNumber = formData.phoneNumber;
              }

              await register(registerData);
              router.push("/uk");
            } catch (err) {
              console.error("Registration error:", err);
            }
          }}
        >
          {(error || validationError) && (
            <div className="text-red-500 text-xs mb-2">{error || validationError}</div>
          )}

          <div>
            <label htmlFor="email" className="block mb-1">
              Email*
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
            <label htmlFor="name" className="block mb-1">
              Your name*
            </label>
            <input
              id="name"
              type="text"
              required
              value={formData.name}
              onChange={(e) => setFormData({ ...formData, name: e.target.value })}
              className="w-full bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
            />
          </div>

          <div>
            <label htmlFor="forName" className="block mb-1">
              Last name*
            </label>
            <input
              id="forName"
              type="text"
              required
              value={formData.forName}
              onChange={(e) => setFormData({ ...formData, forName: e.target.value })}
              className="w-full bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
            />
          </div>

          <div>
            <label htmlFor="dateOfBirth" className="block mb-1">
              Birthday
            </label>
            <input
              id="dateOfBirth"
              type="date"
              value={formData.dateOfBirth}
              onChange={(e) => setFormData({ ...formData, dateOfBirth: e.target.value })}
              className="w-full bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
            />
          </div>

          <div>
            <label htmlFor="gender" className="block mb-1">
              Gender
            </label>
            <select
              id="gender"
              value={formData.gender}
              onChange={(e) => setFormData({ ...formData, gender: e.target.value })}
              className="w-full bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
            >
              <option value="">Select gender</option>
              <option value="Male">Male</option>
              <option value="Female">Female</option>
              <option value="Other">Other</option>
            </select>
          </div>

          <div>
            <label htmlFor="phoneNumber" className="block mb-1">
              Phone number
            </label>
            <input
              id="phoneNumber"
              type="tel"
              value={formData.phoneNumber}
              onChange={(e) => setFormData({ ...formData, phoneNumber: e.target.value })}
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
              required
              minLength={6}
              value={formData.password}
              onChange={(e) => setFormData({ ...formData, password: e.target.value })}
              className="w-full bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
            />
          </div>

          <div>
            <label htmlFor="confirmPassword" className="block mb-1">
              Confirm password*
            </label>
            <input
              id="confirmPassword"
              type="password"
              required
              minLength={6}
              value={formData.confirmPassword}
              onChange={(e) => setFormData({ ...formData, confirmPassword: e.target.value })}
              className="w-full bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
            />
          </div>

          <div className="flex items-center mb-4 text-gray-300 text-xs">
            <input id="news" type="checkbox" className="mr-2" />
            <label htmlFor="news">Let me know about New in</label>
          </div>

          <button
            type="submit"
            disabled={isLoading}
            className="w-full bg-black text-white text-xs py-2 rounded-sm font-normal disabled:opacity-50 disabled:cursor-not-allowed"
          >
            {isLoading ? "Signing up..." : "Sign up"}
          </button>
        </form>
      </div>
    </div>
  );
}