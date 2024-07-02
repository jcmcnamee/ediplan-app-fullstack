import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createEditAsset } from '../../services/apiAssets';
import toast from 'react-hot-toast';

export function useUpdateAsset(category) {
  const queryClient = useQueryClient();

  const { mutate: updateAsset, isPending: isUpdating } = useMutation({
    mutationFn: ({ updatedData, id }) => createEditAsset(updatedData, id, category),
    onSuccess: () => {
      toast.success('New asset successfully updated.');
      queryClient.invalidateQueries({ queryKey: [category] });
    },
    onError: err => {
      toast.error(err.message);
    },
  });

  return { isUpdating, updateAsset };
}
