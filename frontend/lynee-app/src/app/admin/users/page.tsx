"use client";

import {
  Box,
  Typography,
  Stack,
  TextField,
  IconButton,
  Button,
  MenuItem,
  Select,
  Chip,
} from "@mui/material";
import {
  FilterAltOutlined,
  EditOutlined,
  DeleteForeverOutlined,
  ArrowBackIos,
  ArrowForwardIos,
  PanoramaFishEyeOutlined,
  WestOutlined,
} from "@mui/icons-material";
import { useUsersStore } from "@/stores/userStore";
import { useEffect } from "react";

export default function UsersPage() {
  const { users, loading, fetchUsers, deleteUser } = useUsersStore();

  useEffect(() => {
    fetchUsers();
  }, [fetchUsers]);

  //if (loading) return <p>Loading...</p>;
  const userss = [
    {
      name: "Mark Wilson",
      email: "MarkWilson@gmail.com",
      city: "Ukraine",
      date: "Aug 16, 2024",
      status: "Active",
      avatar:
        "https://t3.ftcdn.net/jpg/03/22/53/38/360_F_322533850_Lz5JL2K0nVqL48gjCiRLSa2ssxpdfyer.jpg",
    },
    {
      name: "Maria Johns",
      email: "MariaJohns@gmail.com",
      city: "USA",
      date: "Aug 16, 2024",
      status: "Pending",
      avatar: "https://randomuser.me/api/portraits/women/65.jpg",
    },
    {
      name: "Jony Deph",
      email: "JonyDeph@gmail.com",
      city: "Italy",
      date: "Aug 16, 2024",
      status: "Deleted",
      avatar: "https://randomuser.me/api/portraits/men/45.jpg",
    },
    {
      name: "Lillia Bos",
      email: "LilliaBos@gmail.com",
      city: "Ukraine",
      date: "Aug 16, 2024",
      status: "Active",
      avatar: "https://randomuser.me/api/portraits/women/22.jpg",
    },
  ];

  const getStatusColor = (status: string) => {
    switch (status) {
      case "Active":
        return { bg: "#D4F5D4", color: "#2F8F2F" };
      case "Pending":
        return { bg: "#FFF3C8", color: "#D09B00" };
      case "Deleted":
        return { bg: "#FFD6D6", color: "#B30000" };
      default:
        return { bg: "#eee", color: "#555" };
    }
  };

  return (
    
    <Box sx={{ p: { xs: 2, md: 4 }, backgroundColor: "#FFFFFF" }}>
      <IconButton sx={{ mb: { xs: 1, sm: 2 } }}>
        <PanoramaFishEyeOutlined fontSize="large" sx={{ color: "black" }} />
        <WestOutlined fontSize="large" sx={{ color: "black", ml: -3 }} />
      </IconButton>
      {/* TOP CARDS */}
      <Stack
        direction={{ xs: "column", sm: "row" }}
        spacing={3}
        sx={{ mb: 4, flexWrap: "wrap" }}
      >
        {[
          {
            title: "Total users",
            value: "+1240",
            percent: "+40%",
            color: "#37d67a",
          },
          {
            title: "New users",
            value: "+300",
            percent: "+10%",
            color: "#37d67a",
          },
          {
            title: "Active users",
            value: "650",
            percent: "-5%",
            color: "#ff4d4d",
          },
        ].map((card, idx) => (
          <Box
            key={idx}
            sx={{
              background: "#1A1D23",
              color: "#FFFFFF",
              p: 3,
              borderRadius: 2,
              width: { xs: "100%", sm: "48%", md: 300 },
              boxShadow: "0 2px 10px rgba(0,0,0,0.2)",
            }}
          >
            <Typography sx={{ fontSize: 18, fontWeight: 500 }}>
              {card.title}
            </Typography>
            <Typography sx={{ fontSize: 36, fontWeight: 500 }}>
              {card.value}
            </Typography>
            <Typography
              sx={{ color: card.color, fontSize: 16, fontWeight: 500 }}
            >
              {card.percent} vs last month
            </Typography>
          </Box>
        ))}
      </Stack>

      {/* MAIN CONTENT */}
      <Box
        sx={{
          background: "white",
          p: { xs: 2, md: 3 },
          borderRadius: 2,
          boxShadow: "0 2px 10px rgba(0,0,0,0.1)",
        }}
      >
        {/* Header */}
        <Stack
          direction={{ xs: "column", md: "row" }}
          alignItems={{ xs: "flex-start", md: "center" }}
          justifyContent="space-between"
          sx={{ mb: 2, gap: 2 }}
        >
          <Typography sx={{ fontSize: 25, fontWeight: 500, color: "black" }}>
            Users Management{" "}
            <span
              style={{
                fontSize: 15,
                fontWeight: 400,
                backgroundColor: "black",
                color: "#FFFFFF",
                borderRadius: 3,
                padding: "2px 6px",
              }}
            >
              1240 users
            </span>
          </Typography>

          <Stack direction="row" spacing={2}>
            <Button
              variant="outlined"
              startIcon={<FilterAltOutlined />}
              sx={{
                textTransform: "none",
                fontSize: 16,
                backgroundColor: "#F6F6F6",
                border: "none",
                color: "#2C2B2B",
              }}
            >
              Filters
            </Button>

            <Select
              size="small"
              defaultValue="20 days"
              sx={{
                backgroundColor: "#F6F6F6",
                border: "none",
                color: "#2C2B2B",
                fontWeight: "400",
              }}
            >
              <MenuItem value="20 days">Last 20 Days</MenuItem>
              <MenuItem value="1 month">Last Month</MenuItem>
              <MenuItem value="3 months">Last 3 Months</MenuItem>
            </Select>
          </Stack>
        </Stack>

        {/* Search */}
        <TextField
          placeholder="Search..."
          fullWidth
          size="small"
          sx={{ mb: 3 }}
        />

        {/* TABLE (scrollable on mobile) */}
        <Box sx={{ overflowX: "auto" }}>
          {/* Header */}
          <Stack
            direction="row"
            sx={{
              minWidth: 900,
              py: 1,
              borderBottom: "1px solid #ccc",
              fontWeight: 500,
              color: "#1A1D23",
              fontSize: 18,
            }}
          >
            <Box sx={{ flex: 2 }}>User</Box>
            <Box sx={{ flex: 2 }}>Email address</Box>
            <Box sx={{ flex: 1 }}>City</Box>
            <Box sx={{ flex: 1 }}>Role</Box>
            {/*<Box sx={{ flex: 1 }}>Status</Box>*/}
            <Box sx={{ width: 100 }}></Box>
          </Stack>

          {/* Rows */}
          {users.map((u, i) => {
            //const status = getStatusColor(u.isActive);
            return (
              <Stack
                key={i}
                direction="row"
                alignItems="center"
                sx={{
                  minWidth: 900,
                  py: 2,
                  borderBottom: "1px solid #f0f0f0",
                }}
              >
                {/* User */}
                <Box
                  sx={{
                    flex: 2,
                    display: "flex",
                    alignItems: "center",
                    gap: 2,
                  }}
                >
                  {/*<Box
                    component="img"
                    //src={u.avatar}
                    sx={{
                      width: 60,
                      height: 60,
                      borderRadius: "50%",
                      objectFit: "cover",
                    }}
                  />*/}
                  <Typography sx={{ color: "black", fontSize: 16 }}>
                    {u.name}
                  </Typography>
                </Box>

                {/* Email */}
                <Box sx={{ flex: 2, color: "black", fontSize: 16 }}>
                  {u.email}
                </Box>

                {/* City */}
                <Box sx={{ flex: 1, color: "black", fontSize: 16 }}>
                  {u.address?.city}
                </Box>

                {/* Date */}
                <Box sx={{ flex: 1, color: "black", fontSize: 16 }}>
                  {u.role}
                </Box>

                {/* Status */}
                {/*<Box sx={{ flex: 1 }}>
                  <Chip
                    label={u.isActive}
                    sx={{
                      //background: status.bg,
                      //color: status.color,
                      fontWeight: 500,
                      fontSize: 16,
                      borderRadius: 2,
                    }}
                  />
                </Box>*/}

                {/* Icons */}
                <Stack direction="row" sx={{ width: 100 }} spacing={1}>
                  <IconButton>
                    <EditOutlined />
                  </IconButton>
                  <IconButton onClick={() => {
                if (confirm("Delete this user?")) {
                  deleteUser(u.id);
                }
              }}>
                    <DeleteForeverOutlined />
                  </IconButton>
                </Stack>
              </Stack>
            );
          })}
        </Box>

        {/* Pagination */}
        <Stack
          direction="row"
          spacing={1}
          sx={{ mt: 3, justifyContent: "center" }}
        >
          <IconButton>
            <ArrowBackIos sx={{ color: "#4B4949" }} />
          </IconButton>

          {[1, 2, 3, "...", 8, 9, 10].map((p, i) => (
            <Box
              key={i}
              sx={{
                px: 2,
                py: 1,
                borderRadius: 1,
                cursor: "pointer",
                background: p === 1 ? "#eee" : "white",
                color: "black",
                fontSize: 19,
              }}
            >
              {p}
            </Box>
          ))}

          <IconButton>
            <ArrowForwardIos sx={{ color: "#4B4949" }} />
          </IconButton>
        </Stack>
      </Box>
    </Box>
  );
}
