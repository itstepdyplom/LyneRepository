"use client";

import { Box, Typography, Button, IconButton } from "@mui/material";
import {
  PanoramaFishEyeOutlined,
  WestOutlined
} from "@mui/icons-material";

const items = [
  { name: "Elena Mobith", price: "2500$", payment: "Paid", status: "Deliver" },
  { name: "Martha Hock", price: "500$", payment: "Pending", status: "Pending" },
  { name: "Roberto Umbrace", price: "345$", payment: "Pending", status: "New Order" },
  { name: "Katy Holms", price: "80$", payment: "Paid", status: "Cancelled" },
];

export default function ItemsPage() {
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
            <Typography sx={{ fontWeight: 600, fontSize: 18 }}>Name</Typography>
            <Typography sx={{ fontWeight: 600, fontSize: 18 }}>Amount of money</Typography>
            <Typography sx={{ fontWeight: 600, fontSize: 18 }}>Payment</Typography>
            <Typography sx={{ fontWeight: 600, fontSize: 18 }}>Status</Typography>
          </Box>

          {/* ROWS */}
          {items.map((item, i) => (
            <Box
              key={i}
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
              <Typography sx={{ fontSize: 16 }}>{item.name}</Typography>
              <Typography sx={{ fontSize: 16 }}>{item.price}</Typography>
              <Typography sx={{ fontSize: 16 }}>{item.payment}</Typography>

              <Button
                variant="contained"
                size="small"
                sx={{
                  color: "black",
                  bgcolor:
                    i === 0
                      ? "#A1B9C7"
                      : i === 1
                      ? "#DEB6AC"
                      : i === 2
                      ? "#FECDBE"
                      : "#F57C7C",
                  width: { xs: "100px", md: "140px" },
                  fontSize: { xs: "12px", md: "14px" },
                }}
              >
                {item.status}
              </Button>
            </Box>
          ))}
        </Box>
      </Box>
    </Box>
  );
}
