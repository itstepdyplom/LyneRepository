"use client";
import React from "react";
import Image from "next/image";
import {
  Box,
  Typography,
  Grid,
  Card,
  CardContent,
  Button,
  Divider,
  FormControl,
  Select,
  MenuItem,
} from "@mui/material";
import { useState, useEffect, useRef } from "react";
import { useAuthStore } from "@/stores/authStore";
import { useRouter } from "next/navigation";
import { ordersAPI } from "@/services/api"; // <-- перевір шлях
import { OrderDto } from "@/utils/constants";

export default function AdminMain() {
  const [value, setValue] = useState("Month");
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
    <Box
      sx={{
        display: "flex",
        flexDirection: { xs: "column", md: "row" },
        backgroundColor: "#fff",
        width: "100%",
      }}
    >
      {/* MAIN CONTENT */}
      <Box sx={{ flex: 1, p: 4, overflowY: "auto" }}>
        <Box
          sx={{
            display: "flex",
            flexDirection: { xs: "column", sm: "row" },
            justifyContent: "space-between",
            alignItems: { xs: "flex-start", sm: "center" },
            mb: 4,
          }}
        >
          <Box
            sx={{
              display: "flex",
              alignItems: "center",
              gap: 2,
              color: "black",
            }}
          >
            <Typography sx={{ fontSize: 20, fontWeight: 500 }}>
              Your stats
            </Typography>
            <Box sx={{ ml: "auto", mr: 5 }}>
              <FormControl
                size="small"
                sx={{ minWidth: 170, borderBottom: "1px solid black" }}
              >
                <Select
                  value={value}
                  onChange={(e) => setValue(e.target.value)}
                  displayEmpty
                  sx={{ fontSize: "18px", fontWeight: "500" }}
                >
                  <MenuItem
                    value="Month"
                    sx={{ fontSize: "18px", fontWeight: "500" }}
                  >
                    Month
                  </MenuItem>
                  <MenuItem
                    value="Year"
                    sx={{ fontSize: "18px", fontWeight: "500" }}
                  >
                    Year
                  </MenuItem>
                  <MenuItem
                    value="Week"
                    sx={{ fontSize: "18px", fontWeight: "500" }}
                  >
                    Week
                  </MenuItem>
                  <MenuItem
                    value="Day"
                    sx={{ fontSize: "18px", fontWeight: "500" }}
                  >
                    Day
                  </MenuItem>
                </Select>
              </FormControl>
            </Box>
          </Box>
        </Box>

        {/* Stats Cards */}
        <Grid container spacing={3} sx={{ mb: 4 }}>
          {["+1020 new orders", "+300 new users", "+15000$ revenue"].map(
            (text, i) => (
              <Grid sx={{ xs: 12, md: 4 }} key={i}>
                <Card sx={{ bgcolor: "#1A1D23", color: "white", p: 1 }}>
                  <CardContent>
                    <Typography
                      variant="h5"
                      sx={{ fontWeight: 500, fontSize: "36px" }}
                    >
                      {text.split(" ")[0]}
                    </Typography>
                    <Typography sx={{ fontWeight: 500, fontSize: "20px" }}>
                      {text.split(" ").slice(1).join(" ")}
                    </Typography>
                  </CardContent>
                </Card>
              </Grid>
            )
          )}
        </Grid>

        {/* Revenue chart placeholder */}
        <Box sx={{ mb: 4 }}>
          <Typography variant="h6" sx={{ mb: 2, color: "black" }}>
            Revenue by month
          </Typography>

          <Box
            sx={{
              height: { xs: 200, sm: 260 },
              borderRadius: 2,
              p: 2,
              display: "flex",
              alignItems: "flex-end",
              gap: 2,
              overflowX: "auto",
            }}
          >
            {[
              { label: "Oct", value: 70 },
              { label: "Nov", value: 85 },
              { label: "Dec", value: 75 },
              { label: "Jan", value: 90 },
              { label: "Feb", value: 80 },
              { label: "Mar", value: 95 },
              { label: "Apr", value: 105 },
              { label: "May", value: 113 },
              { label: "Jun", value: 94 },
              { label: "Jul", value: 104 },
              { label: "Aug", value: 113 },
              { label: "Sep", value: 134 },
              { label: "Oct", value: 150 },
            ].map((item, i) => (
              <Box
                key={i}
                sx={{
                  display: "flex",
                  flexDirection: "column",
                  alignItems: "center",
                  flex: 1,
                }}
              >
                <Box
                  sx={{
                    width: "100%",
                    height: `${item.value}px`,
                    backgroundColor: "#E4B9AF",
                    transition: "0.3s",
                  }}
                />
                <Typography sx={{ fontSize: 12, mt: 1, color: "black" }}>
                  {item.label}
                </Typography>
              </Box>
            ))}
          </Box>
        </Box>

        {/* Latest Orders */}
        <Typography variant="h6" sx={{ mb: 2, color: "black" }}>
          Latest orders
        </Typography>
        <Box>
          <Grid
            container
            sx={{
              fontWeight: 600,
              mb: 1,
              color: "black",
              display: "flex",
              justifyContent: "space-between",
            }}
          >
            <Grid sx={{ xs: 4 }}>Number</Grid>
            <Grid sx={{ xs: 4 }}>Payment</Grid>
            <Grid sx={{ xs: 4 }}>Status</Grid>
          </Grid>
          <Divider sx={{ mb: 2 }} />

          {orders.map((item) => (
            <Grid
              key={item.id}
              container
              sx={{
                py: 1,
                alignItems: "center",
                color: "black",
                display: "flex",
                justifyContent: "space-between",
                overflowX: "auto",
              }}
            >
              <Grid sx={{ width: "130px" }}>Order #{item.id}</Grid>

              <Grid>{item.paymentMethod}</Grid>

              <Grid>
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
                    width: "120px",
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
              </Grid>
            </Grid>
          ))}
        </Box>
      </Box>

      {/* RIGHT SIDEBAR */}
      <Box
        sx={{
          width: { xs: "100%", md: 260 },
          borderLeft: "1px solid #ddd",
          p: 3,
        }}
      >
        <Typography sx={{ mb: 3, fontWeight: 600, color: "black" }}>
          Decrease in selling
        </Typography>

        {[
          {
            img: "/img/look/3.png",
            text: "Silver evening earrings with rhinestones",
          },
          { img: "/img/look/1.png", text: "Satin bag in a shade of straw" },
          { img: "/img/newArrivals/2.png", text: "White cotton t-shirt" },
        ].map((item, i) => (
          <Box key={i} sx={{ mb: 4 }}>
            <Box sx={{ position: "relative", width: "100%", height: 200 }}>
              <Image
                src={item.img}
                alt="prod"
                fill
                style={{ objectFit: "contain" }}
              />
            </Box>
            <Typography sx={{ fontSize: 13, mt: 1, color: "black" }}>
              {item.text}
            </Typography>
          </Box>
        ))}

        <Button variant="contained" fullWidth sx={{ backgroundColor: "black" }}>
          Promote now
        </Button>
      </Box>
    </Box>
  );
}
