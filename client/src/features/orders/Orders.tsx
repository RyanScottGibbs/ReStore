import { TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Button } from "@mui/material";
import { useEffect, useState } from "react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { currencyFormat } from "../../app/util/util";
import OrderDetailed from "./OrderDetailed";
import { fetchOrderAsync } from "./orderSlice";

export default function Orders() {
    const {orders, orderLoaded} = useAppSelector(state => state.order);
    const [selectedOrderNumber, setSelectedOrderNumber] = useState(0);
    const dispatch = useAppDispatch();

    useEffect(() => {
        if(!orderLoaded) dispatch(fetchOrderAsync());
    }, [orderLoaded, dispatch]);

    if(!orderLoaded) return <LoadingComponent message='Loading Orders...' />

    return (
        <>
        {selectedOrderNumber > 0 ? (
            <OrderDetailed 
                order={orders?.find(o => o.id === selectedOrderNumber)!}
                setSelectedOrder={setSelectedOrderNumber}
            />
        ) : (
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead>
                    <TableRow>
                        <TableCell>Order Number</TableCell>
                        <TableCell align="right">Total</TableCell>
                        <TableCell align="right">Order Date</TableCell>
                        <TableCell align="right">Order Status</TableCell>
                        <TableCell align="right"></TableCell>
                    </TableRow>
                    </TableHead>
                    <TableBody> 
                        {orders?.map((order) => (
                            <TableRow
                            key={order.id}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                            <TableCell component="th" scope="row">
                                {order.id}
                            </TableCell>
                            <TableCell align="right">{currencyFormat(order.total)}</TableCell>
                            <TableCell align="right">{order.orderDate.split('T')[0]}</TableCell>
                            <TableCell align="right">{order.orderStatus}</TableCell>
                            <TableCell align="right">
                                <Button onClick={() => setSelectedOrderNumber(order.id)}>
                                    View
                                </Button>
                            </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>        
        )}
        </>
    )
}