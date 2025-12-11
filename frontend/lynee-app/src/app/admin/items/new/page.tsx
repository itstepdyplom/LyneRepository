"use client";

import {
  Box,
  Typography,
  TextField,
  Checkbox,
  FormControlLabel,
  Select,
  MenuItem,
  Button,
  Paper,
  IconButton,
} from "@mui/material";
import {
  Add,
  FileUploadOutlined,
  InsertPhotoOutlined,
  PanoramaFishEyeOutlined,
  WestOutlined,
} from "@mui/icons-material";
import { useState } from "react";
import StatusModal from "../StatusModal";

export default function NewItemPage() {
  const [statusOpen, setStatusOpen] = useState(false);

  return (
    <Box sx={{ p: { xs: 2, sm: 4 }, backgroundColor: "#FFFFFF", color: "black" }}>
      {/* Header */}
      <IconButton sx={{ mb: { xs: 1, sm: 2 } }}>
        <PanoramaFishEyeOutlined fontSize="large" sx={{ color: "black" }} />
        <WestOutlined fontSize="large" sx={{ color: "black", ml: -3 }} />
      </IconButton>

      <Box
        sx={{
          display: "flex",
          flexDirection: { xs: "column", sm: "row" },
          alignItems: { xs: "flex-start", sm: "center" },
          gap: { xs: 2, sm: 3 },
          mb: { xs: 2, sm: 4 },
        }}
      >
        <Typography variant="h5" fontWeight={500} sx={{ fontSize: { xs: 20, sm: 24 } }}>
          New item
        </Typography>

        <Box sx={{ display: "flex", gap: 1, flexWrap: "wrap" }}>
          <FormControlLabel
            control={<Checkbox />}
            label="Mass Market"
            sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 18 } }}
          />
          <FormControlLabel
            control={<Checkbox />}
            label="Premium segment"
            sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 18 } }}
          />
        </Box>
      </Box>

      {/* Main Layout */}
      <Box
        sx={{
          display: "flex",
          flexDirection: { xs: "column", md: "row" },
          gap: { xs: 2, md: 4 },
        }}
      >
        {/* LEFT COLUMN */}
        <Box sx={{ flex: 1, display: "flex", flexDirection: "column", gap: { xs: 2, sm: 3 } }}>
          <Box>
            <Typography mb={1} sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}>
              Name of item
            </Typography>
            <TextField fullWidth size="small" sx={{ backgroundColor: "#F6F6F6" }} />
          </Box>

          <Box>
            <Typography mb={1} sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}>
              Short Description
            </Typography>
            <TextField
              fullWidth
              size="small"
              multiline
              minRows={3}
              sx={{ backgroundColor: "#F6F6F6" }}
            />
          </Box>

          {/* Brand / Color */}
          <Box sx={{ display: "flex", gap: 2, flexDirection: { xs: "column", sm: "row" } }}>
            <Box sx={{ flex: 1 }}>
              <Typography mb={1} sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}>
                Brand
              </Typography>
              <Select fullWidth size="small" sx={{ backgroundColor: "#F6F6F6" }}>
                <MenuItem value="brand">Brand</MenuItem>
              </Select>
            </Box>

            <Box sx={{ flex: 1 }}>
              <Typography mb={1} sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}>
                Color
              </Typography>
              <Select fullWidth size="small" sx={{ backgroundColor: "#F6F6F6" }}>
                <MenuItem value="color">Color</MenuItem>
              </Select>
            </Box>
          </Box>

          {/* Quantity / Size */}
          <Box sx={{ display: "flex", gap: 2, flexDirection: { xs: "column", sm: "row" } }}>
            <Box sx={{ flex: 1 }}>
              <Typography mb={1} sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}>
                Quantity
              </Typography>
              <Select fullWidth size="small" sx={{ backgroundColor: "#F6F6F6" }}>
                <MenuItem value="1">1</MenuItem>
              </Select>
            </Box>

            <Box sx={{ flex: 1 }}>
              <Typography mb={1} sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}>
                Size
              </Typography>
              <Select fullWidth size="small" sx={{ backgroundColor: "#F6F6F6" }}>
                <MenuItem value="size">M</MenuItem>
              </Select>
            </Box>
          </Box>

          {/* Code / Price */}
          <Box sx={{ display: "flex", gap: 2, flexDirection: { xs: "column", sm: "row" } }}>
            <Box sx={{ flex: 1 }}>
              <Typography mb={1} sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}>
                Code
              </Typography>
              <TextField fullWidth size="small" sx={{ backgroundColor: "#F6F6F6" }} />
            </Box>

            <Box sx={{ flex: 1 }}>
              <Typography mb={1} sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}>
                Price
              </Typography>
              <Select fullWidth size="small" sx={{ backgroundColor: "#F6F6F6" }}>
                <MenuItem value="100">100</MenuItem>
              </Select>
            </Box>
          </Box>

          {/* Discount block */}
          <Typography fontWeight={500} sx={{ fontSize: { xs: 16, sm: 20 } }}>
            Add discount %
          </Typography>

          <Box sx={{ display: "flex", gap: 2, flexDirection: { xs: "column", sm: "row" } }}>
            <Box sx={{ flex: 1 }}>
              <Typography mb={1} sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}>
                Amount of discount, %
              </Typography>
              <TextField fullWidth size="small" sx={{ backgroundColor: "#F6F6F6" }} />
            </Box>

            <Box sx={{ flex: 1 }}>
              <Typography mb={1} sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}>
                Start date
              </Typography>
              <Select fullWidth size="small" sx={{ backgroundColor: "#F6F6F6" }}>
                <MenuItem value="start">Start</MenuItem>
              </Select>
            </Box>

            <Box sx={{ flex: 1 }}>
              <Typography mb={1} sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}>
                End date
              </Typography>
              <Select fullWidth size="small" sx={{ backgroundColor: "#F6F6F6" }}>
                <MenuItem value="end">End</MenuItem>
              </Select>
            </Box>
          </Box>

          {/* Promote */}
          <Box>
            <Typography fontWeight={500} mb={1} sx={{ fontSize: { xs: 16, sm: 20 } }}>
              Promote the item
            </Typography>
            <FormControlLabel
              control={<Checkbox />}
              label="Add to the banner"
              sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 18 } }}
            />
            <FormControlLabel
              control={<Checkbox />}
              label="Add to the newsletter"
              sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 18 } }}
            />
          </Box>
        </Box>

        {/* RIGHT COLUMN */}
        <Box
          sx={{
            width: { xs: "100%", md: "35%" },
            display: "flex",
            flexDirection: "column",
            gap: { xs: 2, sm: 3 },
          }}
        >
          <Box>
            <Typography mb={1} sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}>
              Fabric composition
            </Typography>
            <TextField fullWidth size="small" multiline minRows={5} sx={{ backgroundColor: "#F6F6F6" }} />
          </Box>

          <Box>
            <Typography mb={1} sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}>
              Add photos & video
            </Typography>

            {/* Main preview */}
            <Paper
              sx={{
                height: { xs: 150, sm: 200 },
                borderRadius: 2,
                border: "1px solid #ddd",
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                backgroundColor: "#F6F6F6",
              }}
            >
              <InsertPhotoOutlined sx={{ width: 130, height: { xs: 100, sm: 200 }, color: "gray" }} />
            </Paper>
          </Box>

          {/* Small previews */}
          <Box sx={{ display: "flex", gap: 2, flexWrap: "wrap" }}>
            {[1, 2, 3].map((i) => (
              <Paper
                key={i}
                sx={{
                  width: { xs: "48%", sm: 160 },
                  height: { xs: 100, sm: 130 },
                  border: "1px solid #ddd",
                  display: "flex",
                  alignItems: "center",
                  justifyContent: "center",
                  backgroundColor: "#F6F6F6",
                }}
              >
                <InsertPhotoOutlined sx={{ width: 130, height: { xs: 80, sm: 100 }, color: "gray" }} />
              </Paper>
            ))}
          </Box>

          {/* Upload buttons */}
          <Box
            sx={{
              display: "flex",
              flexDirection: { xs: "column", sm: "row" },
              justifyContent: "space-between",
              gap: { xs: 1, sm: 0 },
              alignItems: "center",
            }}
          >
            <Button
              startIcon={<FileUploadOutlined />}
              sx={{
                textTransform: "none",
                fontWeight: 400,
                fontSize: { xs: 12, sm: 14 },
                color: "black",
                width: { xs: "100%", sm: "auto" },
              }}
            >
              Upload from your computer
            </Button>

            <Button
              endIcon={<Add />}
              sx={{
                textTransform: "none",
                fontWeight: 400,
                fontSize: { xs: 12, sm: 14 },
                borderRadius: 2,
                px: 2,
                color: "black",
                width: { xs: "100%", sm: "auto" },
              }}
            >
              Add more
            </Button>
          </Box>

          {/* Add Item button */}
          <Button
            onClick={() => setStatusOpen(true)}
            fullWidth
            variant="contained"
            sx={{
              background: "#1A1D23",
              color: "white",
              height: 48,
              borderRadius: 2,
              textTransform: "none",
              fontWeight: 500,
              fontSize: { xs: 14, sm: 16 },
              mt: 2,
            }}
          >
            Add the item
          </Button>
          
          <StatusModal
            open={statusOpen}
            image="/img/newArrivals/1.png"
            message="The item is successfully added to your website"
            onClose={() => setStatusOpen(false)}
          />
        </Box>
      </Box>
    </Box>
  );
}
