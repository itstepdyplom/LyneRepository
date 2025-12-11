"use client";

import React, { useState } from "react";
import {
  Box,
  Typography,
  TextField,
  Button,
  Grid,
  IconButton,
  Avatar,
} from "@mui/material";
import {
  DeleteForeverOutlined,
  BorderColorOutlined,
  PanoramaFishEyeOutlined,
  WestOutlined
} from "@mui/icons-material";
import AddIcon from "@mui/icons-material/Add";
import AddSubcategoryModal from "../AddSubcategoryModal";

export default function EditCategoryPage() {
  const [categoryName, setCategoryName] = useState("WOMAN");
  const [categoryPhoto, setCategoryPhoto] = useState(
    "https://statebird.dk/cdn/shop/files/8_1a433177-1a62-4844-b99a-8583ff102cff.jpg?v=1757950079&width=800"
  );
  const [modalOpen, setModalOpen] = useState(false);

  const subcategories = [
    {
      name: "Outerwear",
      img: "https://m.media-amazon.com/images/I/71fQAYLN9dL._AC_UF894,1000_QL80_.jpg",
    },
    {
      name: "Warmwear",
      img: "https://hanro.com/cdn/shop/files/HANRO_B_W_WoolenSilk_071423_071422_LowRes_900x650px_CPHeader-min.jpg?v=1761739899",
    },
    {
      name: "Dresses & Sets",
      img: "https://www.instyle.com/thmb/6QFUeQUV97_0d1z5FnlZ4aa_Sjo=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/springdressesoutfits-8-8b38b11797a24b4ab012d6dc48ae2781.jpg",
    },
    {
      name: "Tops & Bottoms",
      img: "https://cdn-img.prettylittlething.com/2/c/d/6/2cd691983bbbfde1a3788c5efb7ade9a4c92c134_cnf3882_1_petite_vintage_wash_fray_waist_low_rise_straight_leg_jeans.jpg?imwidth=600",
    },
    {
      name: "Underwear & Swimwear",
      img: "https://xcdn.next.co.uk/Common/Items/Default/Default/ItemImages/3_4Ratio/SearchINT/Lge/925966.jpg?im=Resize,width=450",
    },
    {
      name: "Shoes",
      img: "https://assets.ajio.com/medias/sys_master/root/20240419/mfG5/6621f9f705ac7d77bb185e31/-473Wx593H-466923165-pink-MODEL.jpg",
    },
  ];

  return (
    <Box
      sx={{
        p: { xs: 2, sm: 4 },
        display: "flex",
        flexDirection: { xs: "column", sm: "row" },
        gap: { xs: 3, sm: 6 },
        backgroundColor: "#FFFFFF",
      }}
    >
      {/* LEFT SIDE */}
      <Box sx={{ width: { xs: "100%", sm: "45%" } }}>
        <IconButton sx={{ mb: 2 }}>
          <PanoramaFishEyeOutlined fontSize="large" sx={{ color: "black" }} />
          <WestOutlined fontSize="large" sx={{ color: "black", ml: -3 }} />
        </IconButton>

        <Typography
          sx={{ fontSize: { xs: 20, sm: 25 }, fontWeight: 500, mb: 3, color: "black" }}
        >
          Edit category
        </Typography>

        <Typography sx={{ mb: 1, fontSize: { xs: 14, sm: 16 }, fontWeight: 400, color: "black" }}>
          Name of category
        </Typography>

        <TextField
          fullWidth
          value={categoryName}
          onChange={(e) => setCategoryName(e.target.value)}
          sx={{ mb: 3 }}
        />

        <Typography sx={{ mb: 1, fontSize: { xs: 14, sm: 16 }, fontWeight: 400, color: "black" }}>
          Add photo of the category
        </Typography>

        <Box
          sx={{
            width: "100%",
            height: { xs: 250, sm: 500 },
            overflow: "hidden",
            mb: 1,
          }}
        >
          <img
            src={categoryPhoto}
            alt="category"
            style={{ width: "100%", height: "100%", objectFit: "cover" }}
          />
        </Box>

        <Button
          component="label"
          sx={{
            textTransform: "none",
            fontSize: { xs: 12, sm: 14 },
            fontWeight: 400,
            display: "flex",
            alignItems: "center",
            gap: 1,
            mb: 4,
            color: "black",
          }}
        >
          ⬆ Upload from your computer
          <input hidden type="file" />
        </Button>

        <Button
          variant="contained"
          sx={{
            width: "100%",
            height: 48,
            borderRadius: 1,
            textTransform: "none",
            backgroundColor: "#1A1D23",
            fontWeight: 400,
            fontSize: { xs: 14, sm: 16 },
            "&:hover": { backgroundColor: "#222" },
          }}
        >
          Confirm changes
        </Button>
      </Box>

      {/* RIGHT SIDE */}
      <Box sx={{ flex: 1, mt: { xs: 3, sm: 5 } }}>
        <Box
          sx={{
            display: "flex",
            flexDirection: { xs: "column", sm: "row" },
            justifyContent: "space-between",
            mb: 2,
            alignItems: { xs: "flex-start", sm: "center" },
            gap: { xs: 2, sm: 0 },
          }}
        >
          <Typography sx={{ fontSize: { xs: 16, sm: 20 }, fontWeight: 400, color: "black" }}>
            List of the subcategories
          </Typography>

          <Box sx={{ display: "flex", gap: 1 }}>
            <AddSubcategoryModal
              open={modalOpen}
              onClose={() => setModalOpen(false)}
              onSubmit={(data) => console.log("New subcategory:", data)}
            />
            <Button
              variant="outlined"
              onClick={() => setModalOpen(true)}
              sx={{
                height: 36,
                textTransform: "none",
                px: 2,
                borderRadius: 1,
                display: "flex",
                alignItems: "center",
                gap: 1,
                color: "black",
                borderColor: "black",
                fontSize: { xs: 12, sm: 14 },
              }}
            >
              Add new subcategory <AddIcon fontSize="small" />
            </Button>
          </Box>
        </Box>

        <Box sx={{ display: "flex", flexDirection: "column", gap: 2 }}>
          {subcategories.map((item, idx) => (
            <Box
              key={idx}
              sx={{
                display: "flex",
                flexDirection: { xs: "column", sm: "row" },
                alignItems: { xs: "flex-start", sm: "center" },
                gap: { xs: 1, sm: 2 },
                p: 1,
                borderRadius: 2,
                border: "1px solid #ddd",
              }}
            >
              <Avatar
                src={item.img}
                variant="rounded"
                sx={{ width: { xs: 50, sm: 58 }, height: { xs: 56, sm: 66 } }}
              />

              <Typography
                sx={{
                  flex: 1,
                  fontSize: { xs: 14, sm: 20 },
                  fontWeight: 400,
                  color: "black",
                }}
              >
                {item.name}
              </Typography>

              <Box sx={{ display: "flex", gap: 1 }}>
                <IconButton size="small">
                  <BorderColorOutlined sx={{ color: "black" }} />
                </IconButton>
                <IconButton size="small">
                  <DeleteForeverOutlined sx={{ color: "black" }} />
                </IconButton>
              </Box>
            </Box>
          ))}
        </Box>
      </Box>
    </Box>
  );
}
