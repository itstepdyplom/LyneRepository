"use client";

import React from "react";
import {
  Drawer,
  Box,
  Typography,
  IconButton,
  Button,
  Divider,
  List,
  ListItem,
  ListItemAvatar,
  ListItemText,
  Avatar,
  TextField,
  Stack,
} from "@mui/material";
import {
  Close as CloseIcon,
  Add as AddIcon,
  Remove as RemoveIcon,
  Delete as DeleteIcon,
} from "@mui/icons-material";
import { useCartStore } from "../../stores/cartStore";
import { authAPI, CreateOrderDto, ordersAPI } from "@/services/api";

const CartDrawer: React.FC = () => {
  const isOpen = useCartStore((s) => s.isOpen);
  const closeCart = useCartStore((s) => s.closeCart);
  const removeItem = useCartStore((s) => s.removeItem);
  const updateQuantity = useCartStore((s) => s.updateQuantity);

  const [addressId, setAddressId] = React.useState<number>();

React.useEffect(() => {
  const load = async () => {
    try {
      const me = await authAPI.me();
      setAddressId(me.addressId);
    } catch (e) {
      
    }
  };
  load();
}, []);

  const items = useCartStore((s) => s.items);

  const totalItems = React.useMemo(
    () => items.reduce((sum, i) => sum + i.quantity, 0),
    [items]
  );

  const totalPrice = React.useMemo(
    () => items.reduce((sum, i) => sum + i.price * i.quantity, 0),
    [items]
  );

  const handleQuantityChange = (id: string, newQuantity: number) => {
    if (newQuantity <= 0) {
      removeItem(id);
    } else {
      updateQuantity(id, newQuantity);
    }
  };

  const clearCart = useCartStore((s) => s.clearCart);

  const [isSubmitting, setIsSubmitting] = React.useState(false);
  const [error, setError] = React.useState<string | null>(null);

  const onCheckout = async () => {
    setError(null);
    setIsSubmitting(true);
    try {
      const dto: CreateOrderDto = {
  shippingAddressId: addressId!,
  paymentMethod: "Card",
  items: items.map(i => ({
    productId: String(i.productId),
    quantity: i.quantity,
    unitPrice: i.price,
  })),
};
console.log(dto.items[0].productId);


await ordersAPI.create(dto);

      clearCart();
      closeCart();

      // опціонально: редірект
      // router.push(`/${locale}/orders`);  (якщо є така сторінка)
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    } catch (e: any) {
      setError(e?.message ?? "Checkout failed");
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <Drawer
      anchor="right"
      open={isOpen}
      onClose={closeCart}
      sx={{
        "& .MuiDrawer-paper": {
          width: { xs: "100%", sm: 420 },
          backgroundColor: "background.default",
        },
      }}
    >
      <Box sx={{ height: "100%", display: "flex", flexDirection: "column" }}>
        {/* Header */}
        <Box
          sx={{
            display: "flex",
            alignItems: "center",
            justifyContent: "space-between",
            p: 3,
            borderBottom: "1px solid",
            borderColor: "divider",
          }}
        >
          <Typography
            variant="h6"
            sx={{
              fontWeight: 400,
              letterSpacing: "0.1em",
              fontSize: "1.1rem",
            }}
          >
            SHOPPING BAG ({totalItems})
          </Typography>
          <IconButton onClick={closeCart}>
            <CloseIcon />
          </IconButton>
        </Box>

        {/* Cart Items */}
        <Box sx={{ flex: 1, overflow: "auto" }}>
          {items.length === 0 ? (
            <Box
              sx={{
                display: "flex",
                flexDirection: "column",
                alignItems: "center",
                justifyContent: "center",
                height: "100%",
                px: 3,
                textAlign: "center",
              }}
            >
              <Typography
                variant="h6"
                sx={{
                  mb: 2,
                  fontWeight: 300,
                  color: "text.secondary",
                }}
              >
                Your bag is empty
              </Typography>
              <Typography
                variant="body2"
                sx={{
                  mb: 4,
                  color: "text.secondary",
                  maxWidth: 200,
                }}
              >
                Add some luxury items to get started
              </Typography>
              <Button
                variant="outlined"
                onClick={closeCart}
                sx={{ minWidth: 150 }}
              >
                Continue Shopping
              </Button>
            </Box>
          ) : (
            <List sx={{ p: 0 }}>
              {items.map((item) => (
                <React.Fragment key={item.id}>
                  <ListItem
                    sx={{
                      py: 3,
                      px: 3,
                      alignItems: "flex-start",
                    }}
                  >
                    <ListItemAvatar sx={{ mr: 2 }}>
                      <Avatar
                        src={item.image}
                        alt={item.name}
                        variant="square"
                        sx={{
                          width: 80,
                          height: 80,
                          backgroundColor: "grey.200",
                        }}
                      />
                    </ListItemAvatar>
                    <Box sx={{ flex: 1, minWidth: 0 }}>
                      <ListItemText
                        primary={
                          <Typography
                            variant="subtitle1"
                            component="div"
                            sx={{
                              fontWeight: 500,
                              fontSize: "0.95rem",
                              mb: 0.5,
                            }}
                          >
                            {item.name}
                          </Typography>
                        }
                        secondary={
                          <Box>
                            <Typography
                              variant="body2"
                              component="div"
                              color="text.secondary"
                              sx={{ fontSize: "0.85rem" }}
                            >
                              Size: {item.size} | Color: {item.color}
                            </Typography>

                            <Typography
                              variant="subtitle2"
                              component="div"
                              sx={{
                                fontWeight: 600,
                                color: "primary.main",
                                mt: 1,
                              }}
                            >
                              ${item.price.toLocaleString()}
                            </Typography>
                          </Box>
                        }
                        secondaryTypographyProps={{ component: "div" }} // ✅ ключове
                        primaryTypographyProps={{ component: "div" }} // щоб і primary не робив зайвий <p>
                      />

                      {/* Quantity Controls */}
                      <Stack
                        direction="row"
                        alignItems="center"
                        spacing={1}
                        sx={{ mt: 2 }}
                      >
                        <IconButton
                          size="small"
                          onClick={() =>
                            handleQuantityChange(item.id, item.quantity - 1)
                          }
                          sx={{
                            border: "1px solid",
                            borderColor: "divider",
                            width: 32,
                            height: 32,
                          }}
                        >
                          <RemoveIcon fontSize="small" />
                        </IconButton>

                        <TextField
                          value={item.quantity}
                          size="small"
                          inputProps={{
                            min: 1,
                            style: {
                              textAlign: "center",
                              padding: "8px 4px",
                              width: "40px",
                            },
                          }}
                          onChange={(e) => {
                            const newQuantity = parseInt(e.target.value) || 1;
                            handleQuantityChange(item.id, newQuantity);
                          }}
                          sx={{
                            width: 60,
                            "& .MuiOutlinedInput-root": {
                              "& fieldset": {
                                borderColor: "divider",
                              },
                            },
                          }}
                        />

                        <IconButton
                          size="small"
                          onClick={() =>
                            handleQuantityChange(item.id, item.quantity + 1)
                          }
                          sx={{
                            border: "1px solid",
                            borderColor: "divider",
                            width: 32,
                            height: 32,
                          }}
                        >
                          <AddIcon fontSize="small" />
                        </IconButton>

                        <IconButton
                          size="small"
                          onClick={() => removeItem(item.id)}
                          sx={{
                            ml: 1,
                            color: "error.main",
                          }}
                        >
                          <DeleteIcon fontSize="small" />
                        </IconButton>
                      </Stack>
                    </Box>
                  </ListItem>
                  <Divider />
                </React.Fragment>
              ))}
            </List>
          )}
        </Box>

        {/* Footer with Total and Checkout */}
        {items.length > 0 && (
          <Box
            sx={{
              p: 3,
              borderTop: "1px solid",
              borderColor: "divider",
              backgroundColor: "background.paper",
            }}
          >
            <Box
              sx={{
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
                mb: 3,
              }}
            >
              <Typography
                variant="h6"
                sx={{
                  fontWeight: 400,
                  letterSpacing: "0.05em",
                }}
              >
                TOTAL
              </Typography>
              <Typography
                variant="h6"
                sx={{
                  fontWeight: 600,
                  fontSize: "1.2rem",
                }}
              >
                ${totalPrice.toLocaleString()}
              </Typography>
            </Box>

            <Stack spacing={2}>
              <Button
                variant="contained"
                fullWidth
                size="large"
                disabled={isSubmitting || items.length === 0}
                onClick={onCheckout}
                sx={{
                  py: 1.5,
                  fontSize: "1rem",
                  fontWeight: 500,
                  letterSpacing: "0.05em",
                }}
              >
                {isSubmitting ? "PROCESSING..." : "CHECKOUT"}
              </Button>

              {error && (
                <Typography variant="body2" color="error" sx={{ mt: 1 }}>
                  {error}
                </Typography>
              )}
              <Button
                variant="outlined"
                fullWidth
                size="large"
                onClick={closeCart}
                sx={{
                  py: 1.5,
                  fontSize: "1rem",
                  fontWeight: 500,
                  letterSpacing: "0.05em",
                }}
              >
                CONTINUE SHOPPING
              </Button>
            </Stack>
          </Box>
        )}
      </Box>
    </Drawer>
  );
};

export default CartDrawer;
