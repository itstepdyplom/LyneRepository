"use client";

import {
  Box,
  Typography,
  Button,
  TextField,
  Checkbox,
  FormControlLabel,
  Stack,
  IconButton,
  Divider,
} from "@mui/material";

import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import {
  DeleteForeverOutlined,
  BorderColorOutlined,
  PanoramaFishEyeOutlined,
  WestOutlined
} from "@mui/icons-material";
import { useState } from "react";
import Link from "next/link";
import { useEffect } from "react";
import { useCategoryStore } from "@/stores/categoryStore";

const filters = [
  "Mass Market",
  "Premium segment",
  "Woman",
  "Men",
  "Kids",
  "Accessories",
  "Home",
];

const categories = [
  {
    name: "WOMAN",
    image:
      "https://statebird.dk/cdn/shop/files/8_1a433177-1a62-4844-b99a-8583ff102cff.jpg?v=1757950079&width=800",
    subcategories: [
      {
        name: "Outerwear",
        image:
          "https://m.media-amazon.com/images/I/71fQAYLN9dL._AC_UF894,1000_QL80_.jpg",
      },
      {
        name: "Warmwear",
        image:
          "https://hanro.com/cdn/shop/files/HANRO_B_W_WoolenSilk_071423_071422_LowRes_900x650px_CPHeader-min.jpg?v=1761739899",
      },
      {
        name: "Dresses & Sets",
        image:
          "https://www.instyle.com/thmb/6QFUeQUV97_0d1z5FnlZ4aa_Sjo=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/springdressesoutfits-8-8b38b11797a24b4ab012d6dc48ae2781.jpg",
      },
      {
        name: "Tops & Bottoms",
        image:
          "https://cdn-img.prettylittlething.com/2/c/d/6/2cd691983bbbfde1a3788c5efb7ade9a4c92c134_cnf3882_1_petite_vintage_wash_fray_waist_low_rise_straight_leg_jeans.jpg?imwidth=600",
      },
      {
        name: "Underwear & Swimwear",
        image:
          "https://xcdn.next.co.uk/Common/Items/Default/Default/ItemImages/3_4Ratio/SearchINT/Lge/925966.jpg?im=Resize,width=450",
      },
      {
        name: "Shoes",
        image:
          "https://assets.ajio.com/medias/sys_master/root/20240419/mfG5/6621f9f705ac7d77bb185e31/-473Wx593H-466923165-pink-MODEL.jpg",
      },
    ],
  },
  {
    name: "WOMAN / PREMIUM",
    image:
      "https://img2.ans-media.com/i/840x1260/SS24-POD01C-99X_F1.webp?v=1725461595",
  },
  {
    name: "MEN",
    image:
      "https://cdn.media.amplience.net/i/frasersdev/ralphlauren-preaw-mens-model-linen-043?fmt=auto&upscale=false&w=634&$h-ttl$",
  },
  {
    name: "MEN / PREMIUM",
    image:
      "https://answear.com/blog/wp-content/uploads/2025/11/cover-10-1200x900.jpg",
  },
  {
    name: "KIDS",
    image:
      "https://knittingdoodles.com/cdn/shop/files/SUMMERPMODEL1.jpg?v=1705596075",
  },
  {
    name: "KIDS / PREMIUM",
    image:
      "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcQ5bU1fE2KJZ0afjacFNyyo4u4qW7x8eIOT0QNmaIB0uAt9eldf",
  },
  {
    name: "BAGS AND WALLETS / WOMAN",
    image:
      "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcRpzDBK6mbtk625r3zqQ1RB4ekiMrAClxSt-QNfoPYiy-Y9kTfa",
  },
  {
    name: "BAGS AND WALLETS / WOMAN / PREMIUM",
    image:
      "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcTvkZN7HxyOFQyH-Za73Oi-nDMb-vxFU3gQBxDHJdZ-4KOcTdpc",
  },
];

export default function CategoryPage() {
  const [openIndex, setOpenIndex] = useState<number | null>(null);
  const toggleOpen = (index: number) => setOpenIndex(openIndex === index ? null : index);
  const {
    categories,
    loading,
    error,
    fetchCategories,
    deleteCategory,
  } = useCategoryStore();

  useEffect(() => {
    fetchCategories();
  }, [fetchCategories]);

  return (
    <Box sx={{ p: { xs: 2, sm: 4 }, backgroundColor: "#fff" }}>
        <IconButton sx={{ mb: { xs: 1, sm: 2 } }}>
                <PanoramaFishEyeOutlined fontSize="large" sx={{ color: "black" }} />
                <WestOutlined fontSize="large" sx={{ color: "black", ml: -3 }} />
              </IconButton>
      {/* Search */}
      <Typography sx={{ mb: 2, fontWeight: 500, fontSize: { xs: 20, sm: 25 }, color: "black" }}>
        Search Category
      </Typography>

      <Stack direction={{ xs: "column", sm: "row" }} spacing={2} mb={4}>
        <TextField
          fullWidth
          placeholder="Search..."
          sx={{ background: "#f5f5f5", borderRadius: 1 }}
        />
        <Button
          variant="contained"
          sx={{
            background: "#1A1D23",
            px: { xs: 0, sm: 4 },
            py: { xs: 1.2, sm: "auto" },
            fontWeight: 500,
            fontSize: { xs: 14, sm: 16 },
            width: { xs: "100%", sm: 445 },
            ":hover": { background: "#333" },
          }}
        >
          Add the category
        </Button>
      </Stack>

      {/* Category title */}
      <Typography sx={{ fontWeight: 500, mb: 2, fontSize: { xs: 20, sm: 25 }, color: "black" }}>
        Category
      </Typography>
      <Divider sx={{ mb: 3, backgroundColor: "black" }} />

      {/* Filters */}
      <Stack
        direction={{ xs: "column", sm: "row" }}
        flexWrap="wrap"
        spacing={2}
        mb={2}
        color="black"
      >
        {filters.map((label) => (
          <FormControlLabel
            key={label}
            control={<Checkbox size="small" />}
            label={label}
            sx={{ fontSize: { xs: 12, sm: 16 }, fontWeight: 400 }}
          />
        ))}
      </Stack>

      {/* Categories list */}
      <Box
        sx={{
          maxHeight: "500px",
          overflowY: "auto",
          pr: 1,
          "&::-webkit-scrollbar": { width: "6px" },
          "&::-webkit-scrollbar-track": { background: "transparent" },
          "&::-webkit-scrollbar-thumb": { background: "#c2c2c2", borderRadius: "10px" },
          "&::-webkit-scrollbar-thumb:hover": { background: "#a8a8a8" },
        }}
      >
        <Stack spacing={3}>
          {categories.map((cat) => (
            <Box key={cat.id}>
              {/* CATEGORY ITEM */}
              <Box
                sx={{
                  display: "flex",
                  flexDirection: { xs: "column", sm: "row" },
                  alignItems: { xs: "flex-start", sm: "center" },
                  justifyContent: "space-between",
                  gap: { xs: 1, sm: 2 },
                }}
              >
                <Box sx={{ display: "flex", alignItems: "center", gap: 2 }}>
                  {/*<Box
                    component="img"
                    //src={cat.image}
                    sx={{
                      width: { xs: 50, sm: 58 },
                      height: { xs: 58, sm: 68 },
                      objectFit: "cover",
                      borderRadius: 1,
                    }}
                  />*/}
                  <Typography sx={{ fontWeight: 500, fontSize: { xs: 16, sm: 20 }, color: "black" }}>
                    {cat.name}
                  </Typography>
                </Box>

                {/* Buttons */}
                
                <Stack direction="row" spacing={1} mt={{ xs: 1, sm: 0 }}>
                  {/*{cat.subcategories && (
                    <IconButton onClick={() => toggleOpen(i)}>
                      <ExpandMoreIcon
                        sx={{
                          transform: openIndex === i ? "rotate(180deg)" : "rotate(0deg)",
                          transition: "0.2s",
                          color: "black",
                        }}
                      />
                    </IconButton>
                  )}*/}
                  <Link href={`/admin/category/${cat.id}`} style={{ display: "flex" }}>
                    <IconButton>
                      <BorderColorOutlined sx={{ color: "black" }} />
                    </IconButton>
                  </Link>
                  <IconButton disabled={loading}
              onClick={() => {
                if (confirm("Delete this category?")) {
                  deleteCategory(cat.id);
                }
              }}>
                    <DeleteForeverOutlined sx={{ color: "black" }} />
                  </IconButton>
                </Stack>
              </Box>

              {/* SUBCATEGORIES */}
              {/*
              {cat.subcategories && openIndex === i && (
                <Box
                  sx={{
                    ml: { xs: 2, sm: 10 },
                    mt: 1,
                    mb: 2,
                    display: "flex",
                    flexDirection: "column",
                    gap: 1,
                  }}
                >
                  {cat.subcategories.map((sub, idx) => (
                    <Box
                      key={idx}
                      sx={{
                        display: "flex",
                        flexDirection: { xs: "column", sm: "row" },
                        justifyContent: "space-between",
                        alignItems: { xs: "flex-start", sm: "center" },
                        p: { xs: 1, sm: 1.4 },
                        borderRadius: 1,
                        color: "black",
                        gap: { xs: 1, sm: 2 },
                      }}
                    >
                      <Box sx={{ display: "flex", alignItems: "center", gap: 2 }}>
                        <Box
                          component="img"
                          src={sub.image}
                          sx={{
                            width: { xs: 45, sm: 55 },
                            height: { xs: 50, sm: 63 },
                            objectFit: "cover",
                            borderRadius: 1,
                          }}
                        />
                        <Typography sx={{ fontSize: { xs: 14, sm: 18 }, fontWeight: 400 }}>
                          {sub.name}
                        </Typography>
                      </Box>

                      <Stack direction="row" spacing={1} mt={{ xs: 1, sm: 0 }}>
                        <IconButton size="small">
                          <BorderColorOutlined fontSize="small" sx={{ color: "black" }} />
                        </IconButton>
                        <IconButton size="small">
                          <DeleteForeverOutlined fontSize="small" sx={{ color: "black" }} />
                        </IconButton>
                      </Stack>
                    </Box>
                  ))}
                </Box>
              )}*/}
            </Box>
          ))}
        </Stack>
      </Box>
    </Box>
  );
}