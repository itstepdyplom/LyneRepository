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
import { useState, useEffect } from "react";
import StatusModal from "../StatusModal";
import { uploadImage } from "@/services/api";
import { useProductsStore } from "@/stores/productStore";
import { useCategoryStore } from "@/stores/categoryStore";

export default function NewItemPage() {
  const [statusOpen, setStatusOpen] = useState(false);
  const [form, setForm] = useState({
    name: "",
    description: "",
    price: "",
    brand: "",
    categoryId: "",
    imageUrl: "",
    size: "",
    color: "",
    stockQuantity: "",
    isActive: true,
  });
  const handleFileChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;

    try {
      setUploading(true);
      const url = await uploadImage(file);
      update("imageUrl", url);
    } catch (e) {
      alert("Image upload failed");
    } finally {
      setUploading(false);
    }
  };
  const handleSubmit = async () => {
    if (!form.imageUrl) {
      alert("Upload image first");
      return;
    }

    try {
      await createProduct({
        name: form.name,
        description: form.description,
        price: Number(form.price),
        brand: form.brand,
        categoryId: form.categoryId,
        imageUrl: form.imageUrl,
        size: form.size,
        color: form.color,
        stockQuantity: Number(form.stockQuantity),
        isActive: form.isActive,
      });

      setStatusOpen(true);
    } catch {
      alert("Failed to create product");
    }
  };

  const update = (key: string, value: any) => {
    setForm((prev) => ({ ...prev, [key]: value }));
  };
  const { createProduct, loading } = useProductsStore();
  const [uploading, setUploading] = useState(false);
  const { categories, fetchCategories } = useCategoryStore();

  useEffect(() => {
    fetchCategories();
  }, [fetchCategories]);
  return (
    <Box
      sx={{ p: { xs: 2, sm: 4 }, backgroundColor: "#FFFFFF", color: "black" }}
    >
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
        <Typography
          variant="h5"
          fontWeight={500}
          sx={{ fontSize: { xs: 20, sm: 24 } }}
        >
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
        <Box
          sx={{
            flex: 1,
            display: "flex",
            flexDirection: "column",
            gap: { xs: 2, sm: 3 },
          }}
        >
          <Box>
            <Typography
              mb={1}
              sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}
            >
              Name of item
            </Typography>
            <TextField
              fullWidth
              size="small"
              value={form.name}
              onChange={(e) => update("name", e.target.value)}
              sx={{ backgroundColor: "#F6F6F6" }}
            />
          </Box>

          <Box>
            <Typography
              mb={1}
              sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}
            >
              Short Description
            </Typography>
            <TextField
              fullWidth
              size="small"
              multiline
              minRows={3}
              value={form.description}
              onChange={(e) => update("description", e.target.value)}
              sx={{ backgroundColor: "#F6F6F6" }}
            />
          </Box>

          {/* Brand / Color */}
          <Box
            sx={{
              display: "flex",
              gap: 2,
              flexDirection: { xs: "column", sm: "row" },
            }}
          >
            <Box sx={{ flex: 1 }}>
              <Typography
                mb={1}
                sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}
              >
                Brand
              </Typography>
              <TextField
                fullWidth
                size="small"
                value={form.brand}
                onChange={(e) => update("brand", e.target.value)}
                sx={{ backgroundColor: "#F6F6F6" }}
              />
              
            </Box>

            <Box sx={{ flex: 1 }}>
              <Typography
                mb={1}
                sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}
              >
                Color
              </Typography>
              <TextField
                fullWidth
                size="small"
                value={form.color}
                onChange={(e) => update("color", e.target.value)}
                sx={{ backgroundColor: "#F6F6F6" }}
              />
            </Box>
          </Box>

          {/* Quantity / Size */}
          <Box
            sx={{
              display: "flex",
              gap: 2,
              flexDirection: { xs: "column", sm: "row" },
            }}
          >
            <Box sx={{ flex: 1 }}>
              <Typography
                mb={1}
                sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}
              >
                Quantity
              </Typography>
              <TextField
                fullWidth
                size="small"
                value={form.stockQuantity}
                onChange={(e) => update("stockQuantity", e.target.value)}
                sx={{ backgroundColor: "#F6F6F6" }}
              />
                
            </Box>

            <Box sx={{ flex: 1 }}>
              <Typography
                mb={1}
                sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}
              >
                Size
              </Typography>
              <TextField
                fullWidth
                size="small"
                value={form.size}
                onChange={(e) => update("size", e.target.value)}
                sx={{ backgroundColor: "#F6F6F6" }}
              />
                
            </Box>
          </Box>

          {/* Code / Price */}
          <Box
            sx={{
              display: "flex",
              gap: 2,
              flexDirection: { xs: "column", sm: "row" },
            }}
          >
            <Box sx={{ flex: 1 }}>
              <Typography
                mb={1}
                sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}
              >
                Category
              </Typography>
              <Select
                fullWidth
                size="small"
                value={form.categoryId}
                onChange={(e) => update("categoryId", e.target.value)}
                sx={{ backgroundColor: "#F6F6F6" }}
                displayEmpty
              >
                <MenuItem value="">
                  
                </MenuItem>

                {categories.map((cat) => (
                  <MenuItem key={cat.id} value={cat.id}>
                    {cat.name}
                  </MenuItem>
                ))}
              </Select>
            </Box>

            <Box sx={{ flex: 1 }}>
              <Typography
                mb={1}
                sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}
              >
                Price
              </Typography>
              <TextField
                fullWidth
                size="small"
                value={form.price}
                onChange={(e) => update("price", e.target.value)}
                sx={{ backgroundColor: "#F6F6F6" }}
              />
                
            </Box>
          </Box>

          {/* Discount block */}
          <Typography fontWeight={500} sx={{ fontSize: { xs: 16, sm: 20 } }}>
            Add discount %
          </Typography>

          <Box
            sx={{
              display: "flex",
              gap: 2,
              flexDirection: { xs: "column", sm: "row" },
            }}
          >
            <Box sx={{ flex: 1 }}>
              <Typography
                mb={1}
                sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}
              >
                Amount of discount, %
              </Typography>
              <TextField
                fullWidth
                size="small"
                sx={{ backgroundColor: "#F6F6F6" }}
              />
            </Box>

            <Box sx={{ flex: 1 }}>
              <Typography
                mb={1}
                sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}
              >
                Start date
              </Typography>
              <Select
                fullWidth
                size="small"
                sx={{ backgroundColor: "#F6F6F6" }}
              >
                <MenuItem value="start">Start</MenuItem>
              </Select>
            </Box>

            <Box sx={{ flex: 1 }}>
              <Typography
                mb={1}
                sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}
              >
                End date
              </Typography>
              <Select
                fullWidth
                size="small"
                sx={{ backgroundColor: "#F6F6F6" }}
              >
                <MenuItem value="end">End</MenuItem>
              </Select>
            </Box>
          </Box>

          {/* Promote */}
          <Box>
            <Typography
              fontWeight={500}
              mb={1}
              sx={{ fontSize: { xs: 16, sm: 20 } }}
            >
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
            <Typography
              mb={1}
              sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}
            >
              Fabric composition
            </Typography>
            <TextField
              fullWidth
              size="small"
              multiline
              minRows={5}
              sx={{ backgroundColor: "#F6F6F6" }}
            />
          </Box>

          <Box>
            <Typography
              mb={1}
              sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}
            >
              Add photos & video
            </Typography>

            {/* Main preview */}
            {form.imageUrl && (
              <Box mt={2}>
                <Typography mb={1} fontSize={14}>
                  Preview
                </Typography>
                <Box
                  component="img"
                  src={form.imageUrl}
                  alt="Preview"
                  sx={{
                    width: 120,
                    height: 160,
                    objectFit: "cover",
                    borderRadius: 1,
                    border: "1px solid #e0e0e0",
                  }}
                />
              </Box>
            )}
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
                <InsertPhotoOutlined
                  sx={{
                    width: 130,
                    height: { xs: 80, sm: 100 },
                    color: "gray",
                  }}
                />
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
            {/*<Button
              component="label"
              startIcon={<FileUploadOutlined />}
              disabled={uploading}
            >
              {uploading ? "Uploading..." : "Upload from your computer"}

              <input
                type="file"
                hidden
                accept="image/*"
                onChange={handleFileChange}
              />
            </Button>*/}
            <Box sx={{ flex: 1 }}>
              <Typography
                mb={1}
                sx={{ fontWeight: 400, fontSize: { xs: 14, sm: 16 } }}
              >
                Image URL
              </Typography>

              <TextField
                fullWidth
                size="small"
                placeholder="https://..."
                value={form.imageUrl}
                onChange={(e) => update("imageUrl", e.target.value)}
                sx={{ backgroundColor: "#F6F6F6" }}
              />
            </Box>

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
            onClick={handleSubmit}
            disabled={loading || uploading}
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
