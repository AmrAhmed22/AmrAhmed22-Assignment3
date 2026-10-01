# Stack & Heap — Order Example

Sequence traced:
```
Order o1 = new Order { OrderId = 1, CustomerName = "Ali" };
Order o2 = o1;
o2.IsPaid = true;
```

## Diagram 1 — after `Order o1 = new Order { ... };`

```
STACK                     HEAP
--------------            ----------------------------
o1 --------------------> [ Order object @0x001         ]
                          [ OrderId = 1                 ]
                          [ CustomerName = "Ali"        ]
                          [ Quantity = 0                ]
                          [ UnitPrice = 0               ]
                          [ TotalPrice = 0               ]
                          [ IsPaid = false              ]
                          [ DiscountPercent = 0          ]
                          [ ShippingCity = null          ]
                          [ Priority = '\0'              ]
                          [ ItemCode = 0                 ]
```
`o1` is created on the stack holding the address `0x001`. A new `Order` object is allocated on the heap at that address, with `OrderId` and `CustomerName` set explicitly and every other field left at its default value.

## Diagram 2 — after `Order o2 = o1;`

```
STACK                     HEAP
--------------            ----------------------------
o1 --------------------> [ Order object @0x001         ]
o2 --------------------> [ OrderId = 1                 ]
                          [ CustomerName = "Ali"        ]
                          [ Quantity = 0                ]
                          [ UnitPrice = 0               ]
                          [ TotalPrice = 0               ]
                          [ IsPaid = false              ]
                          [ DiscountPercent = 0          ]
                          [ ShippingCity = null          ]
                          [ Priority = '\0'              ]
                          [ ItemCode = 0                 ]
```
`o2` is added to the stack. It is assigned the *same address* (`0x001`) as `o1` — no new object is created, and no field is copied. Only the address itself was copied, not the data it points to.

## Diagram 3 — after `o2.IsPaid = true;`

```
STACK                     HEAP
--------------            ----------------------------
o1 --------------------> [ Order object @0x001         ]
o2 --------------------> [ OrderId = 1                 ]
                          [ CustomerName = "Ali"        ]
                          [ Quantity = 0                ]
                          [ UnitPrice = 0               ]
                          [ TotalPrice = 0               ]
                          [ IsPaid = true    <-- changed]
                          [ DiscountPercent = 0          ]
                          [ ShippingCity = null          ]
                          [ Priority = '\0'              ]
                          [ ItemCode = 0                 ]
```
The single heap object's `IsPaid` field is updated in place. Since `o1` and `o2` both point at `0x001`, reading `o1.IsPaid` afterward also returns `true` — the change is visible through either variable because there is only one object.

## What would be different with structs?

If `Order` were a `struct` — the same way `Point` is in Part C — then `Order o2 = o1;` would copy **every field value** into a brand-new block of memory for `o2`, instead of copying a heap address. There would be no heap object at all in this picture: both `o1` and `o2` would sit directly on the stack (or inline wherever they're declared) as two fully independent copies, side by side. Diagram 2 would need to show `o2` as its own separate box with its own full set of field values (not an arrow to `o1`'s data), and Diagram 3 would show `o2.IsPaid` flipping to `true` **only inside o2's own box**, while `o1.IsPaid` stays `false` — exactly like `p1.X` and `p2.X` staying independent in the `Point` experiment.
