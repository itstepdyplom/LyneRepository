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
const homeItems = [
  {
    id: 1,
    name: "Cozy Throw Blanket",
    price: "1200 UAH",
    image: "https://www.thespruce.com/thmb/42jcjnJFMSuq8fk-mOJu2HPoeYE=/fit-in/1500x2666/filters:no_upscale():max_bytes(150000):strip_icc()/spr-tier-3-detail-barefoot-dreams-cozychic-ebrockob-001-1-0afaac27840c4f3aba83788113228b62.jpeg",
  },
  {
    id: 2,
    name: "Ceramic Vase",
    price: "800 UAH",
    image: "https://www.nordicpeace.com/cdn/shop/products/nordic-circular-hollow-ceramic-vase-donu_main-1_1800x1800.jpg?v=1650480336",
  },
  {
    id: 3,
    name: "Decorative Pillow",
    price: "450 UAH",
    image: "https://m.media-amazon.com/images/I/71YAboIsyAL.jpg",
  },
  {
    id: 4,
    name: "Wall Art Frame",
    price: "950 UAH",
    image: "https://m.media-amazon.com/images/I/71-JiLZLBbL._AC_UF894,1000_QL80_.jpg",
  },
  {
    id: 5,
    name: "Table Lamp",
    price: "2200 UAH",
    image: "https://media.valuelights.co.uk/26451_DLIFE",
  },
  {
    id: 6,
    name: "Wooden Cutting Board",
    price: "650 UAH",
    image: "https://fathersbuildingfutures.org/wp-content/uploads/2020/09/CBAll-scaled.jpg",
  },
  {
    id: 7,
    name: "Coffee Mug Set",
    price: "500 UAH",
    image: "https://femora.in/cdn/shop/files/FMBNNSHBLKMRL_1.jpg?v=1719041481",
  },
  {
    id: 8,
    name: "Indoor Plant",
    price: "750 UAH",
    image: "https://www.houseplant.co.uk/cdn/shop/files/Areca_Palm_Dypsis_Lutescens_Chrysalidocarpus_Tropical_Indoor_Air_Purifying_Pet_Safe_Beginner_Friendly_Colourful_Houseplant.jpg?v=1756905780&width=533",
  },
  {
    id: 9,
    name: "Candles Set",
    price: "400 UAH",
    image: "https://m.media-amazon.com/images/I/71nWtspF89L.jpg",
  },
  {
    id: 10,
    name: "Wall Clock",
    price: "1300 UAH",
    image: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQh5lbGSwT7Lik2INdOLi6_J57E4gbNoJqHaA&s",
  },
  {
    id: 11,
    name: "Storage Basket",
    price: "850 UAH",
    image: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSH6jr9HtThgvbuFGdBe7ESF2LD3So20B_KmA&s",
  },
  {
    id: 12,
    name: "Floor Rug",
    price: "2400 UAH",
    image: "https://carpetcdn.com/img/catalog-440/bath-mat-16286a-ecru-0.png",
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
        {homeItems.slice(0, page * itemsPerPage).map((item) => (
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