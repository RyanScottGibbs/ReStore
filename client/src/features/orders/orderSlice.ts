import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import agent from "../../app/api/agent";
import { Order } from "../../app/models/order";

interface OrderState {
    orders: Order[] | null;
    status: string;
    orderLoaded: boolean;
}

const initialState: OrderState = {
    orders: null,
    status: 'idle',
    orderLoaded: false
}

export const fetchOrderAsync = createAsyncThunk<Order[]>(
    'order/fetchOrderAsync',
    async (_, thunkAPI) => {
        try {
            return await agent.Orders.list();
        } catch (error: any) {
            return thunkAPI.rejectWithValue({error: error.data})
        }
    }
)

export const orderSlice = createSlice({
    name: 'order',
    initialState,
    reducers: {

    },
    extraReducers: (builder => {
        builder.addCase(fetchOrderAsync.pending, (state) => {
            state.status = "pendingFetchOrder";
            state.orderLoaded = false;
        });
        builder.addCase(fetchOrderAsync.fulfilled, (state, action) => {
            state.orders = action.payload;
            state.status = 'idle';
            state.orderLoaded = true;
        });
        builder.addCase(fetchOrderAsync.rejected, (state, action) => {
            console.log(action.payload);
            state.status = 'idle';
            state.orderLoaded = true;
        });
    })
})