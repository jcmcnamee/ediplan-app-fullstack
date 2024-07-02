import { useQueryClient, useMutation } from "@tanstack/react-query";
import { deleteAsset as deleteAssetApi } from "../../services/apiAssets";
import toast from "react-hot-toast";

export function useDeleteAsset(category) {
  const queryClient = useQueryClient();
  const { mutate: deleteAsset, isLoading: isDeleting } = useMutation({
    mutationFn: deleteAssetApi,
    onSuccess: () => {
      toast.success(`${name} successfully deleted`);

      queryClient.invalidateQueries({ queryKey: [category] });
    },
    onError: (err) => toast.error(err.message),
  });

  return { deleteAsset, isDeleting };
}
