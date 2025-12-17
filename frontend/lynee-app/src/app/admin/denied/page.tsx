import Link from "next/link";
export default function AccessDeniedPage() {
  return (
    <div className="flex min-h-screen items-center justify-center flex-col">
      <h1 className="text-2xl font-semibold mb-4">Access denied</h1>
      <p className="text-gray-600">
        You do not have permission to access the admin panel.
      </p>
      <Link href="/login/account">
      <button className="px-4 py-2 bg-black text-white rounded">
        To login
      </button>
    </Link>
    </div>
  );
}
