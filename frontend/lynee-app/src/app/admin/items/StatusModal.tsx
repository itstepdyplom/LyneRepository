"use client";

import React from "react";
import {
  Box,
  Typography,
  Button,
  Modal,
  Fade,
  Backdrop,
  IconButton,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";

interface StatusModalProps {
  open: boolean;
  image: string;
  message: string;
  onClose: () => void;
}

export default function StatusModal({
  open,
  image,
  message,
  onClose,
}: StatusModalProps) {
  return (
    <Modal
      open={open}
      onClose={onClose}
      closeAfterTransition
      BackdropComponent={Backdrop}
      BackdropProps={{
        timeout: 300,
        sx: {
          backgroundColor: "rgba(255,255,255,0.7)",
        },
      }}
    >
      <Fade in={open}>
        <Box
          sx={{
            position: "fixed",
            top: "50%",
            left: "50%",
            transform: "translate(-50%,-50%)",
            width: 770,
            height: 350,
            bgcolor: "#1A1D23",
            color: "#fff",
            borderRadius: 1,
            display: "flex",
            overflow: "hidden",
          }}
        >
          {/* LEFT: IMAGE */}
          <Box
            sx={{
              width: "50%",
              backgroundColor: "#1A1D23",
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
            }}
          >
            <Box
              component="img"
              src={image}
              sx={{
                width: "80%",
                height: "auto",
                objectFit: "contain",
              }}
            />
          </Box>

          {/* RIGHT: CONTENT */}
          <Box
            sx={{
              width: "70%",
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              alignItems: "center",
              px: 6,
              position: "relative",
            }}
          >
            {/* Close button */}
            <IconButton
              onClick={onClose}
              sx={{
                position: "absolute",
                top: 20,
                right: 20,
                color: "#fff",
              }}
            >
              <CloseIcon />
            </IconButton>

            {/* Text message */}
            <Typography
              sx={{
                color: "#FFFFFF",
                fontWeight: 400,
                fontSize: 19,
                textAlign: "center",
                mb: 3,
                mt: -3,
              }}
            >
              {message}
            </Typography>

            {/* Button */}
            <Button
              onClick={onClose}
              sx={{
                background: "#FFFFFF",
                color: "#2C2B2B",
                textTransform: "none",
                fontWeight: 400,
                fontSize: 20,
                width: 380,
                height: 48,
                borderRadius: 1,
                "&:hover": { background: "#e5e5e5" },
              }}
            >
              View on the website
            </Button>
          </Box>
        </Box>
      </Fade>
    </Modal>
  );
}
