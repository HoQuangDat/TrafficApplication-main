   M S B A M L   `   `   `  ����D  @giaothong, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:giaothong	giaothong  5   	   $    giaothong.EditTeacherGCNWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:giaothong     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	   -   5        $��insertTeacherWindow��5   	     � Title$  EditTeacherGCNWindow��5   	      MaterialDesignFont#���   5   	     teachergcnvm � DataContext# [ 5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency . 
������ $	��620��5   	   $	��720��5   	   .��5      ��  �� CornerRadius$ 10��6   ���#ffffff._�5   
   �  � RowDefinitions 5      �� 5      $��30 �6    6.   �� 5      $��30 �6    6.   �� 5      6"   .+�5      �� $��Right=�6   $��0q�61   
������ $��30��6^   $��50��6j   ��6u   �� *closeTeacherWindow+��6�   �� 	 �� ElementName$	 insertTeacherWindow��.��5      '  !MaterialDesignThemes.Wpf.PackIcon  $��20��6*   $��20��65   ���#08bf96
   Kind$
 Logout=�6V   5       �� 5"      $��1��6   ���#08bf96$��22(�6:   $��Center=�6H   $��Center=�6c   $2��,CẬP NHÂT THÔNG TIN GIẤY CHỨNG NHẬN��6�   $��SemiBold"�6�   6�   � 5$      $��2��6   $��0 10 0 0q�6     � ColumnDefinitions 5%      �� 5&      $	��200 �6'   65   �� 5'      6)   .+�5*      �� $��0 0 1 0q�6   
�������._�5+      �� .+�5,      �� $	��160��6!   $	��160��6.   $ 5��6:   
�������$��1q�6^   $��5q�6r   ._�5-      �� $	��150��6E   $	��150��6R   ��6$   �� *SelectedImage+6a   5.      �� 50        MaterialDesignRaisedButton#��[ 51      $	��100��53       � ToolTip$/ )Resource name: MaterialDesignRaisedButton��54      $��Chọn ảnh��55      $��0q�56      ���#08bf96���#ffffff��52      �� *selectedImage+59      5:      5;      � 5=      $��1��6    5>      �� 5?      6*   �� 5@      $��50 �6(   66   .+�5C      � .+�5D      �� .+�5E      �� $
��10 0q�6)   .+�5F   "   �� $ ��Số giấy chứng nhận��6,   $��16(�6F   $��SemiBold"�6T   6l   �� 5G   "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5H   &   �� {�.  $��30��5K   '   $��14(�5L   '   $	��2 0q�5M   '   $	��5 0q�5N   '   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$  Số giấy chứng nhận��5O   '   ��5I   '   �� *GiayCN.SoGCN+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�5O   '   5P   #   5Q      �� 5S      $
��10 0q�6)   .+�5T   "   �� $$��Số quyết định cấp GCN��6,   $��16(�6I   $��SemiBold"�6W   6o   �� 5U   "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5V   &   �� $��30��5X   )   $��14(�5Y   )   $	��2 0q�5Z   )   $	��5 0q�5[   )   $$ Số quyết định cấp GCN��5\   )   ��5W   )   �� *GiayCN.QDCap+$ PropertyChanged=�$ TwoWay=�5\   )   5]   #   5^      �� 5`      $
��10 0q�6)   .+�5a   "   �� $��Đơn vị cấp��6,   $��16(�6>   $��SemiBold"�6L   6d   �� 5b   "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5c   &   �� $��30��5e   )   $��14(�5f   )   $	��2 0q�5g   )   $	��5 0q�5h   )   $ Đơn vị cấp��5i   )   ��5d   )   �� *GiayCN.DonViCap+$ PropertyChanged=�$ TwoWay=�5i   )   5j   #   5k      � 5m      .+�5n   "   �� $
��10 0q�6-   .+�5o   &   �� $��Ngày cấp��60   $��16(�6@   $��SemiBold"�6N   6f   �� 5p   &   $ 5��6-   $
��0 10q�6>   $��1 1 1 1q�6L   
�������._�5q   *   (  "System.Windows.Controls.DatePicker  $	��234��5r   +   $��30��5s   +   $��14(�5t   +   $	��2 0q�5v   +   $	��5 0q�5w   +   $ Ngày cấp��5x   +      Text 5u   +   �� *GiayCN.NgayCap+$ PropertyChanged=�$ TwoWay=�5x   +   5y   '   5z   #   �� 5|   "   $
��10 0q�6-   .+�5}   &   �� $��Ngày kiểm tra��60   $��16(�6E   $��SemiBold"�6S   6k   �� 5~   &   $ 5��6-   $
��0 10q�6>   $��1 1 1 1q�6L   
�������._�5   *     $	��234��5�   +   $��30��5�   +   $��14(�5�   +   $	��2 0q�5�   +   $	��5 0q�5�   +   $ Ngày kiểm tra��5�   +    5�   +   �� *GiayCN.NgayKiemTra+$ PropertyChanged=�$ TwoWay=�5�   +   5�   '   5�   #   5�      �� 5�      $
��10 0q�6)   .+�5�   "   �� $��	Hạng xe��6,   $��16(�6;   $��SemiBold"�6I   6a   �� 5�   "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5�   &   ��  �� DisplayMemberPath$	 ten��5�   )    �� SelectedValuePath$	 ten��5�   )   $��30��5�   )   $	��5 6q�5�   )   $ 	Hạng xe��5�   )   ��5�   )   �� *
ListHangXe+ �� SelectedItem 5�   )   �� *SelectedIndexHangXe+$ PropertyChanged=�$ TwoWay=�5�   '   5�   #   5�      �� 5�      $
��10 0q�6)   .+�5�   "   �� $!��Mã đơn vị tập huấn��6,   $��16(�6F   $��SemiBold"�6T   6l   �� 5�   "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5�   &   �� $��30��5�   '   $��14(�5�   '   $	��2 0q�5�   '   $	��5 0q�5�   '   $! Mã đơn vị tập huấn��5�   '   ��5�   '   �� *GiayCN.MaDonViTapHuan+$ PropertyChanged=�$ TwoWay=�5�   '   5�   #   5�      �� 5�      $
��10 0q�6)   .+�5�   "   �� $��Mã đơn vị kiểm tra��6,   $��16(�6F   $��SemiBold"�6T   6l   �� 5�   "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5�   &   �� $��30��5�   '   $��14(�5�   '   $	��2 0q�5�   '   $	��5 0q�5�   '   $ Mã đơn vị kiểm tra��5�   '   ��5�   '   �� *GiayCN.MaDVKiemTra+$ PropertyChanged=�$ TwoWay=�5�   '   5�   #   5�      5�      5�      � 5�      $��1��6   .+�5�      ��  �� Orientation$ 
Horizontal=�6%   $
��Left=�6>   $��10 0 0 0q�6Y   .+�5�      �� #��[ 5�   !   $	��100��5�   !   $��0 0 10 0q�5�   !   $( "Xóa giáo viên khỏi danh sách��5�   !   $
��Xóa��5�   !   $��0q�5�   !   ���#B31312���#ffffff��5�   !   �� *	removeGCN+��5�   !   �� $	 insertTeacherWindow��5�      5�      �� 5�      $ 
Horizontal=�6%   $��Right=�6>   $��0 0 10 0q�6Z   .+�5�      �� #��[ 5�   %   $	��100��5�   %   $/ )Resource name: MaterialDesignRaisedButton��5�   %   $��Hủy��5�   %   $��0q�5�   %   
�������$��0 0 10 0q�5�   %   ���#ffffff��5�   %   �� *closeTeacherWindow+��5�   %   �� $	 insertTeacherWindow��5�      �� 5�      #��[ 5�   %   $	��150��5�   %   $/ )Resource name: MaterialDesignRaisedButton��5�   %   $��Cập nhật��5�   %   $��0q�5�   %   ���#08bf96���#ffffff��5�   %   �� $	 insertTeacherWindow����5�   %   �� *	editGCN+5�      5�      5�      5�      5�      5�      5�      5�      