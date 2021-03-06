import React from 'react';
import { DeleteIcon } from '../icons/DeleteIcon';
import { EditIcon } from '../icons/EditIcon';

export function CustomerRow({customer, onEdit, onDelete}) {
  return (
    <tr>
      <td>
        <EditIcon onStartEditing={onEdit} />
        <DeleteIcon onDelete={onDelete} />
      </td>
      <td>{customer.name}</td>
      <td>{customer.email}</td>
      <td>{customer.streetAddress}</td>
      <td>{customer.city}</td>
      <td>{customer.region}</td>
    </tr>
  );
}