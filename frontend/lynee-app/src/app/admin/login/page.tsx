"use client";

import React from "react";
import {
  Box,
  TextField,
  Button,
  Typography,
  Checkbox,
  FormControlLabel,
} from "@mui/material";
import Image from "next/image";

export default function AdminLoginPage() {
  return (
    <Box
      sx={{
        width: "100%",
        minHeight: "100vh",
        backgroundColor: "#F5F5F5",
        position: "relative",
        overflow: "hidden",
      }}
    >
      {/* Content Wrapper */}
      <Box
        sx={{
          position: "relative",
          zIndex: 1,
          width: "100%",
          minHeight: "100vh",
          display: "flex",
          flexDirection: "column",
          justifyContent: "center",
          alignItems: "center",
          px: 2,
          py: 6,
          backgroundImage: "url(/img/background.png)",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
        }}
      >
        {/* Background image */}
        <Image
          src="/img/loginback.png"
          alt="photo"
          fill
          style={{
            objectFit: "cover",
            opacity: 0.5,
          }}
        />
        {/* Logo */}
        <Typography
          variant="h4"
          fontWeight={500}
          sx={{
            fontSize: { xs: "40px", sm: "50px", md: "64px" },
            mb: 3,
            color: "black",
          }}
        >
          LYNE
        </Typography>

        {/* Form Box */}
        <Box
          sx={{
            width: { xs: "90%", sm: 450, md: 600 },
            padding: { xs: 3, sm: 4 },
            borderRadius: 2,
            backdropFilter: "blur(5px)",
            display: "flex",
            flexDirection: "column",
            gap: 2,
          }}
        >
          <Typography
            textAlign="center"
            mb={2}
            color="black"
            sx={{
              fontSize: { xs: "20px", sm: "24px" },
              fontWeight: 400,
            }}
          >
            Login Page for Admin
          </Typography>

          <TextField
            label="Email"
            fullWidth
            sx={{ backgroundColor: "white" }}
          />
          <TextField
            label="Password"
            type="password"
            fullWidth
            sx={{ backgroundColor: "white" }}
          />

          <Typography
            component="a"
            href="#"
            sx={{
              fontSize: 14,
              textAlign: "right",
              textDecoration: "underline",
              cursor: "pointer",
              color: "black",
            }}
          >
            Forgot password?
          </Typography>

          <FormControlLabel
            control={<Checkbox />}
            label="Remember me"
            sx={{ color: "black" }}
          />

          <Button
            variant="contained"
            fullWidth
            sx={{
              backgroundColor: "#1A1D23",
              height: 48,
              fontSize: 16,
              textTransform: "none",
            }}
          >
            Log in
          </Button>
        </Box>
      </Box>
    </Box>
  );
}
