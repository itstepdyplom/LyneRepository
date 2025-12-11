"use client";

import { Box, Typography } from "@mui/material";
import Link from "next/link";
import {
  HomeOutlined,
  SellOutlined,
  ShoppingBagOutlined,
  PersonOutline,
  Inventory2Outlined,
  FilterAltOutlined,
  CategoryOutlined,
  LogoutOutlined,
} from "@mui/icons-material";

const menu = [
  { label: "Main", icon: <HomeOutlined />, href: "/admin" },
  { label: "Selling", icon: <SellOutlined />, href: "/admin/selling" },
  { label: "Orders", icon: <ShoppingBagOutlined />, href: "/admin/orders" },
  { label: "Users", icon: <PersonOutline />, href: "/admin/users" },
  { label: "Items", icon: <Inventory2Outlined />, href: "/admin/items" },
  { label: "Filters", icon: <FilterAltOutlined />, href: "/admin/filters" },
  { label: "Category", icon: <CategoryOutlined />, href: "/admin/category" },
];

export default function AdminSidebar() {
  return (
    <Box
      sx={{
        width: 300,
        backgroundImage: "url(/img/background.png)",
        backgroundSize: "cover",
                backgroundRepeat: "no-repeat",
        p: 3,
        display: "flex",
        flexDirection: "column",
        justifyContent: "space-between",
      }}
    >
      <Box>
        <Typography
          variant="h4"
          sx={{ fontWeight: 500, color: "black", fontSize: "64px" }}
        >
          LYNE
        </Typography>
        <Typography
          sx={{ mb: 4, fontWeight: 300, color: "black", fontSize: "20px" }}
        >
          Concept store
        </Typography>

        {menu.map((item) => (
          <Link
            key={item.label}
            href={item.href}
            style={{ textDecoration: "none" }}
          >
            <Box
              sx={{
                display: "flex",
                alignItems: "center",
                gap: 1.5,
                my: 3,
                cursor: "pointer",
                color: "black",
                "&:hover": { opacity: 0.7 },
              }}
            >
              {item.icon}
              <Typography sx={{ fontSize: "22px", fontWeight: "400" }}>
                {item.label}
              </Typography>
            </Box>
          </Link>
        ))}
      </Box>

      <Typography
        sx={{
          cursor: "pointer",
          color: "black",
          fontSize: "20px",
          fontWeight: "400",
        }}
      >
        <LogoutOutlined />
        Log out
      </Typography>
    </Box>
  );
}
