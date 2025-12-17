"use client";

import { Box, Typography, Button, IconButton } from "@mui/material";
import {
  PanoramaFishEyeOutlined,
  WestOutlined
} from "@mui/icons-material";
import { useState, useEffect, useRef } from "react";
import { useAuthStore } from "@/stores/authStore";
import { useRouter } from "next/navigation";
import { ordersAPI } from "@/services/api"; // <-- перевір шлях
import { OrderDto } from "@/utils/constants";

const items = [
  { name: "Elena Mobith", price: "2500$", payment: "Paid", status: "Deliver" },
  { name: "Martha Hock", price: "500$", payment: "Pending", status: "Pending" },
  { name: "Roberto Umbrace", price: "345$", payment: "Pending", status: "New Order" },
  { name: "Katy Holms", price: "80$", payment: "Paid", status: "Cancelled" },
];

export default function ItemsPage() {
  const {
      user,
      isAuthenticated,
      loadingAction,
      hasCheckedAuth,
      checkAuth,
      logout,
    } = useAuthStore();
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
      if (!hasCheckedAuth) return;
      if (loadingAction !== null) return;
      if (!isAuthenticated) {
        router.push("/uk/auth/login");
      }
    }, [loadingAction, isAuthenticated, hasCheckedAuth, router]);
  
    // fetch orders only when user opens "orders" tab and user is authenticated
    useEffect(() => {
      const load = async () => {
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
    }, [isAuthenticated]);
  return (
    <Box sx={{ width: "100%", height: "100vh", backgroundColor: "#fff", p: 2 }}>
      
      <Box
        sx={{
          width: "100%",
          background: "white",
          borderRadius: 2,
          overflow: "hidden",
          
        }}
      >
        <Box
          sx={{
            overflowX: "auto",
            overflowY: "auto",
            maxHeight: "75vh",
          }}
        >
        <IconButton sx={{ mb: { xs: 1, sm: 2 } }}>
        <PanoramaFishEyeOutlined fontSize="large" sx={{ color: "black" }} />
        <WestOutlined fontSize="large" sx={{ color: "black", ml: -3 }} />
      </IconButton>
          {/* HEADER */}
          <Box
            sx={{
              display: "grid",
              gridTemplateColumns: {
                xs: "200px 150px 150px 150px",
                sm: "280px 1fr 1fr 1fr",
                md: "350px 1fr 1fr 1fr",
                lg: "480px 1fr 1fr 1fr",
              },
              px: 2,
              py: 1.5,
              minWidth: "700px",
              backgroundColor: "#fff",
              borderBottom: "1px solid #E5E7EB",
              color: "black",
            }}
          >
            <Typography sx={{ fontWeight: 600, fontSize: 18 }}>Number</Typography>
            <Typography sx={{ fontWeight: 600, fontSize: 18 }}>Date</Typography>
            <Typography sx={{ fontWeight: 600, fontSize: 18 }}>Payment</Typography>
            <Typography sx={{ fontWeight: 600, fontSize: 18 }}>Status</Typography>
          </Box>

          {/* ROWS */}
          {orders.map((item) => (
            <Box
              key={item.id}
              sx={{
                display: "grid",
                gridTemplateColumns: {
                  xs: "200px 150px 150px 150px",
                  sm: "280px 1fr 1fr 1fr",
                  md: "350px 1fr 1fr 1fr",
                  lg: "480px 1fr 1fr 1fr",
                },
                minWidth: "700px",
                px: 2,
                py: 2,
                borderBottom: "1px solid #E5E7EB",
                alignItems: "center",
                color: "black",
              }}
            >
              <Typography sx={{ fontSize: 16 }}>Order #{item.id}</Typography>
              <Typography sx={{ fontSize: 16 }}> {new Date(item.date).toLocaleDateString()}</Typography>
              <Typography sx={{ fontSize: 16 }}>{item.paymentMethod}</Typography>

              <Button
                variant="contained"
                size="small"
                sx={{
                  color: "black",
                  bgcolor:
                    item.orderStatus === 0
                      ? "#A1B9C7"
                      : item.orderStatus === 1
                      ? "#DEB6AC"
                      : item.orderStatus === 2
                      ? "#FECDBE"
                      : "#F57C7C",
                  width: { xs: "100px", md: "140px" },
                  fontSize: { xs: "12px", md: "14px" },
                }}
              >
                  {item.orderStatus === 0
                    ? "Deliver"
                    : item.orderStatus === 1
                    ? "Pending"
                    : item.orderStatus === 2
                    ? "New order"
                    : "Cancelled"}
              </Button>
            </Box>
          ))}
        </Box>
      </Box>
    </Box>
  );
}
