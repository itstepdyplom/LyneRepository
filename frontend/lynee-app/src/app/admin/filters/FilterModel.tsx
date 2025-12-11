"use client";

import React from "react";
import {
  Box,
  Typography,
  TextField,
  Button,
  IconButton,
  Modal,
  Fade,
  Backdrop,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";

interface SortModalProps {
  open: boolean;
  filterName: string;
  onClose: () => void;
  onConfirm: (value: string) => void;
}
export default function SortModal({ open,filterName, onClose, onConfirm }: SortModalProps) {
  const [value, setValue] = React.useState("");

  return (
    <Modal
      open={open}
      onClose={onClose}
      closeAfterTransition
      BackdropComponent={Backdrop}
      BackdropProps={{ timeout: 300,
    sx: {
      backgroundColor: "rgba(255,255,255,0.3)",
    }, }}
    >
      <Fade in={open}>
        <Box
          sx={{
            position: "fixed",
            top: "50%",
            left: "50%",
            transform: "translate(-50%,-50%)",
            width: 450,
            bgcolor: "#2C2B2B",
            color: "#FFFFFF",
            borderRadius: 1,
            p: 4,
            boxShadow: 24,
          }}
        >
          <Box sx={{ display: "flex", justifyContent: "space-between", mb: 3 }}>
            <Typography sx={{ fontSize: 24, fontWeight: 500 }}>
              {filterName} <span style={{ fontWeight: 300 }}>filter</span>
            </Typography>
            <IconButton onClick={onClose} size="small" sx={{ color: "white" }}>
              <CloseIcon />
            </IconButton>
          </Box>

          <Typography sx={{ mb: 1, fontWeight:400, fontSize:16 }}>Name of filter item</Typography>
          <TextField
            fullWidth
            size="small"
            value={value}
            onChange={(e) => setValue(e.target.value)}
            sx={{ background: "#F6F6F6", borderRadius: 1 }}
          />

          <Button
            fullWidth
            onClick={() => onConfirm(value)}
            sx={{
              mt: 3,
              background: "#F6F6F6",
              color: "#2C2B2B",
              textTransform: "none",
              height: 42,
              borderRadius: 1,
              "&:hover": { background: "#e5e5e5" },
            }}
          >
            Confirm
          </Button>
        </Box>
      </Fade>
    </Modal>
  );
}
