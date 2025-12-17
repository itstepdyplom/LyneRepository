"use client";

import { Box, Typography } from "@mui/material";
import {
  AccountCircleOutlined,
  NotificationsNoneOutlined,
} from "@mui/icons-material";
import { useAuthStore } from "@/stores/authStore";

export default function HeaderAdmin() {
  const { user } = useAuthStore();
  return (
    <Box
      sx={{
        display: "flex",
        alignItems: "center",
        gap: 2,
        backgroundColor: "#fff",
      }}
    >
      <Box
        sx={{
          width: 50,
          height: 50,
          borderRadius: "50%",
          background: "black",
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          mt: 5,
          ml: 2,
        }}
      >
        <AccountCircleOutlined sx={{ color: "#c4c4c4", fontSize: 30 }} />
      </Box>

      <Box>
        <Typography
          sx={{ fontSize: 16, fontWeight: 400, color: "black", mt: 3 }}
        >
          {user?.name || "Unknown User"}
        </Typography>
        <Typography sx={{ fontSize: 14, fontWeight: 400, color: "black" }}> {user?.role || "user"}</Typography>
      </Box>
      <Box sx={{ ml: "auto", mr: 5  }}>
        <NotificationsNoneOutlined sx={{ color: "black", fontSize: 28 }} />
      </Box>
    </Box>
  );
}
