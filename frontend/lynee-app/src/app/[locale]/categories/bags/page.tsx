"use client"
import React from 'react';
import { Box, Typography, IconButton,
  Pagination,
  Button, Grid } from '@mui/material';
import { useTranslations } from 'next-intl';
import FavoriteBorderIcon from "@mui/icons-material/FavoriteBorder";
import ViewModuleIcon from "@mui/icons-material/ViewModule";
import ViewListIcon from "@mui/icons-material/ViewList";
import ShoppingBagOutlinedIcon from '@mui/icons-material/ShoppingBagOutlined';
import TuneOutlinedIcon from '@mui/icons-material/TuneOutlined';
import { useState } from "react";
const bagsWalletsItems = [
  {
    id: 1,
    name: "Leather Handbag",
    price: "5200 UAH",
    image: "https://atpatelier.com/cdn/shop/products/Arezzo_Brandy_Vacchetta_Handbag_Front.jpg?v=1678205144",
  },
  {
    id: 2,
    name: "Canvas Tote Bag",
    price: "1800 UAH",
    image: "https://bag-bag.com.ua/content/images/16/1200x1500l80br0/74307127911943.jpg",
  },
  {
    id: 3,
    name: "Elegant Wallet",
    price: "2200 UAH",
    image: "https://lamartina.com/cdn/shop/files/442165a8529589ab3a3bd2b08f555c91.jpg?v=1761588531&width=2048",
  },
  {
    id: 4,
    name: "Backpack",
    price: "3500 UAH",
    image: "https://m.media-amazon.com/images/I/711vhCj9WCL._AC_UY1000_.jpg",
  },
  {
    id: 5,
    name: "Clutch Bag",
    price: "2600 UAH",
    image: "https://rosieanddott.com/cdn/shop/files/FullSizeRender_d0e4b60d-9eb0-49d0-94f4-e94ee2ae3854.jpg?v=1722253023&width=1946",
  },
  {
    id: 6,
    name: "Leather Belt Bag",
    price: "2100 UAH",
    image: "https://urbansouthern.com/cdn/shop/products/studio_half_moon_belt_bag_front3.jpg?v=1721155972&width=1946",
  },
  {
    id: 7,
    name: "Messenger Bag",
    price: "4800 UAH",
    image: "https://bag-bag.com.ua/content/images/25/357x480l50nn0/85393024211881.jpg",
  },
  {
    id: 8,
    name: "Coin Purse",
    price: "750 UAH",
    image: "https://wrapables.com/cdn/shop/products/A73383_G_1600x.jpg?v=1657232377",
  },
  {
    id: 9,
    name: "Travel Duffel Bag",
    price: "6200 UAH",
    image: "https://m.media-amazon.com/images/I/71UwwijAOAL._AC_UY1000_.jpg",
  },
  {
    id: 10,
    name: "Mini Backpack",
    price: "2900 UAH",
    image: "https://m.media-amazon.com/images/I/61pz4pLftaL._AC_UY1000_.jpg",
  },
  {
    id: 11,
    name: "Wristlet",
    price: "1200 UAH",
    image: "https://secretangel.kiev.ua/image/cache/catalog/image/cache/catalog/panty/new/08.04/1244/blesk/newbra/17.09/newpanty09/pjsetviki/newoutlet/1324/12345pj/pantynoshow/mewoutlet/newmist/duhi/sikretk/trusikibantiki/newpj/bravse/kyb-photo/newpic/panty-trusiki/cosm/2908new/1109/zipwallert7-1000x1340.webp",
  },
  {
    id: 12,
    name: "Shoulder Bag",
    price: "4300 UAH",
    image: "https://shop.mango.com/assets/rcs/pics/static/T8/fotos/S/87043278_90_B.jpg?imwidth=2048&imdensity=1&ts=1732635332947",
  },
];

export default function AccessoriesPage() {
  const t = useTranslations('Categories');
const [view, setView] = useState<"four" | "two">("four");
  const [page, setPage] = useState(1);
  const itemsPerPage = 12;

  const handleChangePage = (_: any, value: number) => setPage(value);

  return (
    <Box sx={{ width: "100%", p: 3 }}>
      {/* Header */}
      <Box
        sx={{
          mb: 3,
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
        }}
      >
        <Typography sx={{ fontSize: 16, fontWeight:400 }}>
          <IconButton size="medium">
                <TuneOutlinedIcon fontSize="medium" />
            </IconButton>
            Filters & Sort
        </Typography>
        <Typography variant="h6" sx={{ fontWeight: 400, fontSize:24, mr:12 }}>
          Home
        </Typography>

        <Box sx={{ display: "flex", gap: 1 }}>
          <IconButton onClick={() => setView("four")}>
            <ViewModuleIcon />
          </IconButton>
          <IconButton onClick={() => setView("two")}>
            <ViewListIcon />
          </IconButton>
        </Box>
      </Box>

      {/* Grid */}
      <Grid
        container
        spacing={3}
        sx={{
          display: "grid",
          gridTemplateColumns:
            view === "four"
        ? { xs: "1fr", sm: "1fr 1fr", md: "repeat(4, 1fr)" } 
        : { xs: "1fr", sm: "1fr 1fr", md: "1fr 1fr" },
          gap: 3,
        }}
      >
        {bagsWalletsItems.slice(0, page * itemsPerPage).map((item) => (
          <Box
            key={item.id}
            sx={{
              borderRadius: 2,
              overflow: "hidden",
              position: "relative",
              background: "#fff",
              boxShadow: "0 2px 5px rgba(0,0,0,0.05)",
            }}
          >
            {/* Top Icons */}
            <Box
              sx={{
                position: "absolute",
                top: 10,
                right: 10,
                display: "flex",
                gap: 1,
              }}
            >
              <IconButton size="medium">
                <ShoppingBagOutlinedIcon fontSize="medium" />
              </IconButton>
              <IconButton size="medium">
                <FavoriteBorderIcon fontSize="medium" />
              </IconButton>
            </Box>

            <Box 
              component="img"
              src={item.image}
              alt={item.name} 
              sx={{
                width: "100%",
                height: view === "four" ? 380 : 500,
                objectFit: "cover",
                backgroundColor:"#F8F8F8"
              }} >
            </Box>
            {/* Title + Price */}
            <Box sx={{ p: 2, display:"flex", flexDirection:"column", alignItems:"center" }}>
              <Typography sx={{ fontSize: 16, color:"#2C2B2B" }}>{item.name}</Typography>
              <Typography sx={{ fontSize: 16, fontWeight: 400, color:"#7B8487"  }}>
                {item.price}
              </Typography>
            </Box>
          </Box>
        ))}
      </Grid>

      {/* Load more */}
      <Box sx={{ textAlign: "center", mt: 4 }}>
        <Button variant="text" sx={{color:"#817E7E"}}>Load more</Button>

        <Pagination
          count={5}
          page={page}
          onChange={handleChangePage}
          sx={{ mt: 2, display: "flex", justifyContent: "center", fontSize:16, fontWeight:300, color:"#817E7E" }}
        />
      </Box>
    </Box>
  );
} 