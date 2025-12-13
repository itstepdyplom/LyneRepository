import axios, {
  AxiosInstance,
  AxiosResponse,
  AxiosError,
  InternalAxiosRequestConfig,
} from "axios";

const BASE_URL =
  process.env.NEXT_PUBLIC_API_URL || "https://localhost:5050/api";

const apiClient: AxiosInstance = axios.create({
  baseURL: BASE_URL,
  timeout: 60000,
  headers: {
    "Content-Type": "application/json",
  },
});

apiClient.interceptors.request.use(
  (config: InternalAxiosRequestConfig): InternalAxiosRequestConfig => {
    const token = localStorage.getItem("accessToken");
    if (token && config.headers) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
  },
  (error: AxiosError) => {
    console.error("❌ Request Error:", error);
    return Promise.reject(error);
  }
);

apiClient.interceptors.response.use(
  (r) => r,
  (error) => {
    if (axios.isAxiosError(error)) {
      console.error("AXIOS ERROR", {
        code: error.code,
        message: error.message,
        url: error.config?.url,
        method: error.config?.method,
        hasResponse: !!error.response,
        status: error.response?.status,
      });
    } else {
      console.error("NON-AXIOS ERROR", error);
    }
    return Promise.reject(error);
  }
);

apiClient.interceptors.response.use(
  (response: AxiosResponse) => {
    if (process.env.NODE_ENV === "development") {
      console.log("✅ Response:", {
        status: response.status,
        data: response.data,
      });
    }

    return response;
  },
  async (error: AxiosError) => {
    if (error.response?.status === 401) {
      localStorage.removeItem("accessToken");
      window.location.href = "/uk/auth/login";
    }

    let errorMessage = "An unexpected error occurred";

    if (error.response) {
      errorMessage =
        (error.response.data as { message?: string })?.message ||
        `Server Error: ${error.response.status}`;
    } else if (error.request) {
      errorMessage = "Network error - please check your connection";
    } else {
      errorMessage = error.message || "Request setup error";
    }

    console.error("❌ Response Error:", {
      message: errorMessage,
      status: error.response?.status,
      data: error.response?.data,
    });

    const enhancedError = {
      ...error,
      message: errorMessage,
    };

    return Promise.reject(enhancedError);
  }
);

export default apiClient;
