"use client";

import {
  Box,
  Typography,
  Stack,
  TextField,
  IconButton,
  Button,
  Divider,
} from "@mui/material";
import { Add, EditOutlined, DeleteForeverOutlined } from "@mui/icons-material";
import SortModal from "./FilterModel";
import { useState } from "react";

export default function FiltersPage() {
  const [open, setOpen] = useState(false);
  const [currentFilter, setCurrentFilter] = useState<string | null>(null);

  const filters = {
    sortBy: ["Recommended", "Top Rated", "Price Low to High", "Price High to Low", "New Arrivals", "Best Sellers"],
    category: ["Women", "Men", "Kids", "Accessories", "Home"],
    brand: ["Channel", "Balenciaga", "Burberry", "Valentino", "Manolo Blanik", "Estee Lauder", "Mont Blanc", "Prada", "Jimmy Choo", "Ralf Lauren", "Louis Vuitton", "Dior"],
    size: ["XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "XXXXL"],
  };

  // Блок колонки
  const Column = (title: string, items: string[]) => (
    <Box
      sx={{
        background: "#F6F6F6",
        p: 2,
        borderRadius: 2,
        border: "1px solid #eee",
        display: "flex",
        flexDirection: "column",
        flex: 1,
        minWidth: { xs: 250, sm: 300 },
      }}
    >
      {/* Title */}
      <Stack
        direction="row"
        alignItems="center"
        justifyContent="space-between"
        sx={{ mb: 1 }}
      >
        <Typography sx={{ fontSize: { xs: 16, sm: 18, md: 20 }, fontWeight: 500, color: "black" }}>
          {title}
        </Typography>

        <IconButton
          onClick={() => {
            setCurrentFilter(title);
            setOpen(true);
          }}
          sx={{
            background: "linear-gradient(0deg, #E8BAAF, #A9BACA)",
            width: { xs: 48, sm: 64 },
            height: { xs: 24, sm: 28 },
            borderRadius: "2px",
            color: "white",
          }}
        >
          <Add fontSize="small" />
        </IconButton>
        <SortModal
          open={open}
          filterName={currentFilter ?? ""}
          onClose={() => setOpen(false)}
          onConfirm={(name) => {
            console.log("Filter name:", name);
            setOpen(false);
          }}
        />
      </Stack>

      <Divider sx={{ mb: 1 }} />

      {/* Items inside */}
      <Stack spacing={1}>
        {items.map((i, idx) => (
          <Stack
            key={idx}
            direction="row"
            alignItems="center"
            justifyContent="space-between"
            sx={{ pt: 1 }}
          >
            <Typography sx={{ fontSize: { xs: 12, sm: 14, md: 16 }, fontWeight: 400, color: "black" }}>
              {i}
            </Typography>

            <Stack direction="row" spacing={1}>
              <IconButton size="small">
                <EditOutlined fontSize="small" sx={{ color: "black" }} />
              </IconButton>
              <IconButton size="small">
                <DeleteForeverOutlined fontSize="small" sx={{ color: "black" }} />
              </IconButton>
            </Stack>
          </Stack>
        ))}
      </Stack>
    </Box>
  );

  return (
    <Box sx={{ p: { xs: 2, sm: 4 }, backgroundColor: "#FFFFFF", minHeight: "100vh", width:"100%" }}>
      {/* Title */}
      <Typography sx={{ fontSize: { xs: 20, sm: 22, md: 25 }, fontWeight: 500, mb: 2, color: "black" }}>
        Search Filters
      </Typography>

      {/* Search bar + Button */}
      <Stack direction={{ xs: "column", sm: "row" }} spacing={2} sx={{ mb: 3 }}>
        <TextField
          placeholder="Search..."
          fullWidth
          sx={{
            background: "white",
            borderRadius: 1,
          }}
        />
        <Button
          variant="contained"
          sx={{
            background: "#1a1a1a",
            textTransform: "none",
            px: { xs: 0, sm: 4 },
            py: { xs: 1.2, sm: "auto" },
            fontSize: { xs: 14, sm: 16 },
            fontWeight: 500,
            "&:hover": { background: "#1A1D23" },
            width: { xs: "100%", sm: 445 },
          }}
        >
          Add new filter
        </Button>
      </Stack>

      {/* Columns */}
      <Stack
        direction={{ xs: "column", md: "row" }}
        spacing={{ xs: 2, md: 4 }}
        sx={{ overflowX: { xs: "auto", md: "visible" }, pb: 2 }}
      >
        {Column("Sort by", filters.sortBy)}
        {Column("Category", filters.category)}
        {Column("Brand", filters.brand)}
        {Column("Size", filters.size)}
      </Stack>
    </Box>
  );
}
