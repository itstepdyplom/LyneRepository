"use client";

import {
  Box,
  Container,
  Typography,
  Button,
  Divider,
  Accordion,
  AccordionSummary,
  AccordionDetails,
  ToggleButtonGroup,
  ToggleButton,
  Grid,
  IconButton,
  FormControl,
  Select,
  MenuItem,
} from "@mui/material";
import { FavoriteBorder, LocalMallOutlined, ArrowDownward } from "@mui/icons-material";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import Image from "next/image";
import { useEffect } from "react";
import ImageSlider from "@/components/ImageSlider";
import { useTranslations } from 'next-intl';

export default function ProductPage() {
  const t = useTranslations('Product');
  const product = {
    brand: "Polo Ralph Lauren",
    title: "Cotton Oxford Tie-Front Cropped Shirt",
    price: "6400 UAH",
    images: [
      "/img/newArrivals/1.png",
      "/img/newArrivals/1(1).png",
      "/img/newArrivals/1(2).png",
    ],
    colors: ["#ffffff", "#365896", "#CCAAC8"],
    size: ["34", "35", "36"],
  };

  const related = [
    {
      id: 1,
      title: "SATIN CORSET",
      image: "/img/look/1.png",
      price: "10 000",
    },
    { id: 2, title: "MIDI DRESS", image: "/img/look/2.png", price: "7000" },
    {
      id: 3,
      title: "SILVER EVENING",
      image: "/img/look/3.png",
      price: "20 418",
    },
    {
      id: 4,
      title: "WHITE MAXI DRESS",
      image: "/img/look/4.png",
      price: "7000",
    },
  ];

  const recently = [
    {
      id: 1,
      title: "STYLISH BLACK BAG",
      image: "/img/look/5.png",
      price: "10 000",
      background: "/img/background.png",
    },
    {
      id: 2,
      title: "ELEGANT FASHION DRESS",
      image: "/img/look/6.png",
      price: "7000",
    },
    {
      id: 3,
      title: "POLO ID CALFSKIN",
      image: "/img/look/1.png",
      price: "20 418",
      background: "/img/background.png",
    },
    {
      id: 4,
      title: "STYLISH JEANS SKIRT",
      image: "/img/look/7.png",
      price: "7000",
    },
  ];

  useEffect(() => {
    const header = document.querySelector(
      "header, .MuiAppBar-root"
    ) as HTMLElement | null;
    if (header) {
      header.style.backgroundColor = "transparent";
      header.style.boxShadow = "none";
      header.style.position = "absolute";
      header.style.border = "transparent";
    }

    return () => {
      if (header) {
        header.style.backgroundColor = "white";
        header.style.color = "black";
        header.style.position = "fixed";
      }
    };
  }, []);

  return (
    <Box sx={{ backgroundColor: "#fff", overflowX: "hidden" }}>
      <Box
        sx={{
          height: { xs: '35vh', md: '65vh' },
          position: "relative",
          backgroundImage: "url(/img/background.png)",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
          backgroundPosition: "center",
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          mb: {xs: 4, md: 8},

          "&::before": {
            content: '"Ralph Lauren"',
            position: "absolute",
            left: 0,
            top: 80,
            backgroundColor: "white",
            color: "black",
            fontSize: { xs: '0.8rem', md: '1.1rem' },
            fontWeight: 500,
            padding: "10px 40px 10px 10px",
            clipPath: "polygon(0 0, 100% 0, 80% 50%, 100% 100%, 0 100%)",
            boxShadow: "2px 2px 4px rgba(0,0,0,0.1)",
          },
        }}
      >
        <Box sx={{ mt: {xs:2, sm:3, md:5}, width: "100%" }}>
          <ImageSlider images={product.images} />
        </Box>
      </Box>

      {/*Left block */}
      <Box>
        <Box sx={{ ml: 10 }}>
          <Grid
            container
            spacing={6}
            sx={{ mb: 10, display: "flex", justifyContent: "left" }}
          >
            <Grid sx={{ xs: 12, md: 6, mr: {xs:20, md:40} }}>
              <Typography variant="h5" fontWeight="300">
                {product.brand}
              </Typography>
              <Typography variant="h4" fontWeight="500" gutterBottom>
                {product.title}
              </Typography>
              <Typography variant="h5" fontWeight="500" gutterBottom>
                {product.price}
              </Typography>

              <FormControl variant="standard" sx={{ width: "100%" }}>
                <Box
                  sx={{
                    width: "100%",
                    borderBottom: "1px solid lightgray",
                    pb: 1,
                    display: "flex",
                    justifyContent: "space-between",
                    alignItems: "center",
                  }}
                >
                  <Typography sx={{ fontSize: {xs:"0.5rem", md:"0.9rem"}, color: "#333", fontWeight:"400" }}>
                    {t('selectSize')}
                  </Typography>

                  <Select
                    defaultValue=""
                    displayEmpty
                    disableUnderline
                    IconComponent={ ArrowDownward}
                    sx={{
                      fontSize: "16px",
                      "& .MuiSelect-select": {
                        padding: 0,
                        paddingRight: "24px !important",
                      },
                      "& .MuiSelect-icon": {
                        right: 0,
                      },
                    }}
                  >
                    <MenuItem disabled value="">
                      35
                    </MenuItem>
                    <MenuItem value={35}>35</MenuItem>
                    <MenuItem value={36}>36</MenuItem>
                    <MenuItem value={37}>37</MenuItem>
                    <MenuItem value={38}>38</MenuItem>
                  </Select>
                </Box>
              </FormControl>

              <Typography sx={{ mt: 3, fontSize: {xs:"0.5rem", md:"0.9rem"}, fontWeight:"400" }}>
                {t('selectColor')}
              </Typography>
              <ToggleButtonGroup
                sx={{
                  mt: 1,
                  display: "flex",
                  gap: 1,
                  "& .MuiToggleButtonGroup-grouped": {
                    border: "1px solid lightgray",
                    borderRadius: "50%",
                    padding: 0,
                    minWidth: 0,
                  },
                }}
              >
                {product.colors.map((color) => (
                  <ToggleButton
                    key={color}
                    value={color}
                    sx={{
                      width: 25,
                      height: 25,
                      borderRadius: "50%",
                      bgcolor: color,
                      border: "1px solid black",
                      "&:hover": {
                        border: "2px solid gray",
                      },
                      "&.Mui-selected": {
                        border: "2px solid black",
                        bgcolor: color,
                      },
                      "& .MuiTouchRipple-root": {
                        display: "none",
                      },
                    }}
                  />
                ))}
              </ToggleButtonGroup>
            </Grid>

            {/*Right block*/}
            <Grid sx={{ xs: 12, md: 6, maxWidth: "700px" }}>
              <Box
                sx={{ display: "flex", gap: 2, mb: 3, flexDirection: "column" }}
              >
                <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
                  <Button
                    variant="contained"
                    sx={{
                      flex: 1,
                      backgroundColor: "black",
                      color: "white",
                      py: 1.5,
                      "&:hover": { backgroundColor: "#333" },
                    }}
                  >
                     {t('addToBag')}
                  </Button>
                  <IconButton
                    size="large"
                    sx={{
                      width: 46,
                      height: 46,
                      borderRadius: "4px",
                      background:
                        "linear-gradient(135deg, #FECBBB, #BAA3A9, #95AEBC)",
                      color: "white",
                      boxShadow: "0 4px 8px rgba(0,0,0,0.15)",
                      "&:hover": {
                        transform: "scale(1.05)",
                        boxShadow: "0 6px 12px rgba(0,0,0,0.2)",
                      },
                      transition: "all 0.2s ease-in-out",
                    }}
                  >
                    <FavoriteBorder fontSize="medium" />
                  </IconButton>
                </Box>
                <Button
                  variant="outlined"
                  sx={{
                    flex: 1,
                    color: "black",
                    borderColor: "black",
                    py: 1.5,
                  }}
                  startIcon={
                    <img
                      src="https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/Apple_Pay_logo.svg/2560px-Apple_Pay_logo.svg.png"
                      alt="Apple Pay"
                      width={60}
                    />
                  }
                ></Button>
              </Box>
              <Typography
                variant="h5"
                gutterBottom
                sx={{
                  width: "700px",
                  fontWeight: 300,
                  fontSize: "19px",
                  mt: 7,
                }}
              >
                Polo's classic cotton oxford shirt is updated for the season
                with a cropped silhouette that features self-ties at the front
                waist. Our embroidered Pony adorns the chest for a signature
                finish.
              </Typography>
              {/* Accordion */}
              <Accordion>
                <AccordionSummary expandIcon={<ExpandMoreIcon />}>
                 {t('productDetails')}
                </AccordionSummary>
                <AccordionDetails>
                  Classic cropped shirt with front tie closure and embroidered
                  logo.
                </AccordionDetails>
              </Accordion>

              <Accordion>
                <AccordionSummary expandIcon={<ExpandMoreIcon />}>
                  {t('deliveryAndReturns')}
                </AccordionSummary>
                <AccordionDetails>
                  {t('deliveryDetails')}
                </AccordionDetails>
              </Accordion>

              <Accordion>
                <AccordionSummary expandIcon={<ExpandMoreIcon />}>
                  {t('paymentOptions')}
                </AccordionSummary>
                <AccordionDetails>
                 {t('paymentDetails')}
                </AccordionDetails>
              </Accordion>
            </Grid>
          </Grid>
        </Box>

        {/* Complete look */}
        <Container maxWidth="xl" sx={{ py: { xs: 6, md: 10 },  }}>
          <Typography
            variant="h4"
            gutterBottom
            sx={{textAlign: "center", fontWeight: "500", fontSize: {xs:"1.1rem", md:"1.6rem"}, mb:{xs:4, md:8} }}
          >
           {t('completeLook')}
          </Typography>
          <Box
            sx={{
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
            }}
          >
            <Grid container spacing={2}>
              {related.map((item) => (
                <Grid sx={{ xs: 12, sm: 6, md: 3 }} key={item.id}>
                  <Box textAlign="center" sx={{ minWidth: 280, textAlign: "center" }}>
                    <Box position="relative"  height={350} >
                      <Image
                        src={item.image}
                        alt={item.title}
                        fill
                        style={{ objectFit: "cover", borderRadius: 8 }}
                      />
                    </Box>
                    <Typography variant="body2" sx={{ mt: 1 }}>
                      {item.title}
                    </Typography>
                  </Box>
                </Grid>
              ))}
            </Grid>
          </Box>
        </Container>

        <Divider sx={{ my: 4 }} />

        {/* Recently viewed */}
        <Container
          maxWidth="xl"
          sx={{py: { xs: 6, md: 10 } }}
          
        >
          <Box
            sx={{
              display: "flex",
              alignItems: "flex-start",
            }}
          >
            <Box sx={{ minWidth: "320px", mt: 20 }}>
              <Typography
                variant="h4"
                sx={{
                  fontSize: {xs:"1.8rem", md:"2.3rem"},
                  fontWeight: 500,
                }}
              >
                {t('recently')}
                <br />
                {t('viewed')}
              </Typography>
            </Box>

            <Box sx={{ display: "flex", overflowX: "auto", gap: 2, pb: 2 }}>
          {recently.map((item, i) => {
            const isWhite = i % 2 === 0;
            return (
              <Box key={item.id} sx={{ minWidth: 280, textAlign: "center" }}>
                <Box
                  sx={{
                    width: "100%",
                    height: 450,
                    position: "relative",
                    backgroundImage: item.background ? `url(${item.background})` : "none",
                    backgroundSize: "cover",
                    backgroundPosition: "center",
                  }}
                >
                  <Box sx={{ position: "absolute", top: 8, right: 8, display: "flex"}}>
                    <IconButton size="small" sx={{ color: isWhite ? "white" : "gray", "&:hover": { backgroundColor: "#e0e0e0" } }}>
                      <LocalMallOutlined fontSize="medium" />
                    </IconButton>
                    <IconButton size="small" sx={{ color: isWhite ? "white" : "gray", "&:hover": { backgroundColor: "#e0e0e0" } }}>
                      <FavoriteBorder fontSize="medium" />
                    </IconButton>
                  </Box>
                  <Image src={item.image} alt={item.title} fill style={{ objectFit: "cover" }} />
                </Box>
                <Typography sx={{ mt: 2, fontWeight: 300, textTransform: "uppercase" }}>{item.title}</Typography>
                <Typography sx={{ color: "text.secondary", mt: 0.5 }}>{item.price} UAH</Typography>
              </Box>
            );
          })}
        </Box>
        </Box>
        </Container>
      </Box>
    </Box>
  );
}
