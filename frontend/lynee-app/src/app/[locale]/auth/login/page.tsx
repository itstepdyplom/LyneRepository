import React from "react";
import { useTranslations } from "next-intl";

export default function LoginPage() {
  return (
    <div className="flex min-h-screen font-base">
      {/* Ліва картинка (показується тільки на md і вище) */}
      <div className="w-1/2 hidden md:block">
        <img
          src="https://storage.googleapis.com/a1aa/image/d3a9287d-4100-4b5d-fd26-7afaccb6db7e.jpg"
          alt="Model in blue denim dress with sunglasses standing on beige background"
          className="w-full h-full object-cover"
        />
      </div>

      {/* Права частина з формою */}
      <div className="w-full md:w-1/2 p-8 md:p-16">
        <h1
          className="
            text-black
            font-base
            font-normal
            text-[36px]
            leading-[100%]
            tracking-[0]
            mb-6
          "
        >
          {"Your profile"}
        </h1>

        {/* Tabs */}
        <div className="flex border-b border-black mb-6 text-black text-sm font-normal">
          <button className="border-b-2 border-black pb-1 mr-6">
            {"I am alreedy a user"}
          </button>
          <button className="pb-1">{"Create an account"}</button>
        </div>

        {/* Google button */}
        <button
          type="button"
          className="flex items-center gap-2 text-black text-sm font-normal mb-4"
        >
          <img
            src="https://storage.googleapis.com/a1aa/image/2c210019-b3f6-4ecd-4776-650d84fb9385.jpg"
            alt="Google G logo icon"
            className="w-5 h-5"
          />
          {"Log in with Google"}
        </button>

        <p className="text-xs text-black mb-4">or</p>

        {/* Login form */}
        <form className="space-y-4 text-xs text-gray-400 font-normal">
          <div>
            <label htmlFor="email" className="block mb-1">
              {"Email"}
            </label>
            <input
              id="email"
              type="email"
              className="w-full bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
            />
          </div>

          <div>
            <label htmlFor="password" className="block mb-1">
              {"Password"}
            </label>
            <input
              id="password"
              type="password"
              className="w-full bg-gray-50 border border-gray-100 rounded-sm px-3 py-2 text-black text-xs"
            />
          </div>

          <div className="text-xs text-gray-300 mb-2 cursor-pointer">
            {"Forgot a password?"}
          </div>

          <div className="flex items-center mb-6 text-gray-300 text-xs">
            <input id="remember" type="checkbox" className="mr-2" />
            <label htmlFor="remember">{"Remember me"}</label>
          </div>

          <button
            type="submit"
            className="w-full bg-black text-white text-xs py-2 rounded-sm font-normal"
          >
            {"Log in"}
          </button>
        </form>
      </div>
    </div>
  );
}
