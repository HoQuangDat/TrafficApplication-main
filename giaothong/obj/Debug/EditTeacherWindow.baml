   M S B A M L   `   `   `  ����D  @giaothong, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:giaothong	giaothong  5   	   !    giaothong.EditTeacherWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:giaothong     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5        $��editTeacherWindow��5   	     � Title$  EditTeacherWindow��5
   	      MaterialDesignFont#���   5   	     	teachervm � DataContext# [ 5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency . 
������ $	��650��5   	   $
��1180��5   	   .��5      ��  �� CornerRadius$ 10��6   ���#ffffff._�5   
   �  � RowDefinitions 5      �� 5      $��30 �6    6.   �� 5      $��30 �6    6.   �� 5      6"   .+�5      �� $��Right=�6   $��0q�61   
������ $��30��6^   $��50��6j   ��6u   �� *closeInsertTeacherWindow+��6�   �� 	 �� ElementName$	 editTeacherWindow��.��5      '  !MaterialDesignThemes.Wpf.PackIcon  $��20��6*   $��20��65   ���#08bf96
   Kind$
 Logout=�6V   5       �� 5"      $��1��6   ���#08bf96$��22(�6:   $��Center=�6H   $��Center=�6c   $)��#CẬP NHẬT THÔNG TIN GIÁO VIÊN��6�   $��SemiBold"�6�   6�   � 5$      $��2��6    � ColumnDefinitions 5%      �� 5&      $	��200 �6'   65   �� 5'      6)   .+�5*      �� $��0 0 1 0q�6   
�������._�5+      �� .+�5,      �� $	 100��6!   $	��150��64   $	��150��6A   
�������$��1q�6`   $��	0 15 0 15q�6t   ._�5-      �� $	��150��6j   $	��150��6w   ��6$   �� *SelectedImage+ �� UpdateSourceTrigger$ PropertyChanged=�6�   5.      �� 50        MaterialDesignRaisedButton#��[ 51      $	��100��53       � ToolTip$/ )Resource name: MaterialDesignRaisedButton��54      $��Chọn ảnh��55      $��0q�56      ���#08bf96���#ffffff��52      �� *selectedImage+59      5:      5;      � 5=      $��1��6    5>      �� 5?      6*   �� 5@      $��50 �6(   66   .+�5C      �  5D      �� 5E      61   �� 5F      61   .+�5I      �� .+�5J      �� $
��10 0q�6)   .+�5K   "   �� $��Mã giáo viên��6,   $��16(�6@   $��SemiBold"�6N   6f   �� 5L   "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5M   &   �� {�.  $��30��5P   '   $��14(�5Q   '   $	��2 0q�5R   '   $	��5 0q�5S   '   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Mã giáo viên��5T   '   ��5N   '   �� *GiaoVien.MaGV+$ PropertyChanged=� �� Mode$ TwoWay=�5T   '   5U   #   5V      � 5X      .+�5Y   "   �� $
��10 0q�6-   .+�5Z   &   �� $��Họ đệm��60   $��16(�6>   $��SemiBold"�6L   6d   �� 5[   &   $ 5��6-   $
��0 10q�6>   $��1 1 1 1q�6L   
�������._�5\   *   �� $	��219��5^   '   $��30��5_   '   $��14(�5`   '   $	��2 0q�5a   '   $	��5 0q�5b   '   $ Họ đệm��5c   '   ��5]   '   �� *GiaoVien.HoDem+$ PropertyChanged=�$ TwoWay=�5c   '   5d   '   5e   #   �� 5g   "   $
��10 0q�6-   .+�5h   &   �� $
��Tên��60   $��16(�6;   $��SemiBold"�6I   6a   �� 5i   &   $ 5��6-   $
��0 10q�6>   $��1 1 1 1q�6L   
�������._�5j   *   �� $	��219��5l   '   $��30��5m   '   $��14(�5n   '   $	��2 0q�5o   '   $	��5 0q�5p   '   $
 Tên��5q   '   ��5k   '   �� *GiaoVien.TenGV+$ PropertyChanged=�$ TwoWay=�5q   '   5r   '   5s   #   5t      �� 5v      $
��10 0q�6)   .+�5w   "   �� $��
Ngày sinh��6,   $��16(�6=   $��SemiBold"�6K   6c   �� 5x   "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5y   &   (  "System.Windows.Controls.DatePicker  $��30��5{   '   $��14(�5|   '   $	��2 0q�5}   '   $	��5 0q�5~   '   $ 
Ngày sinh��5   '      Text 5z   )   �� *GiaoVien.NgaySinh+$ PropertyChanged=�$ TwoWay=�5   '   5�   #   5�      �� 5�      $
��10 0q�6)   .+�5�   "   �� $��Số CCCD/CMND��6,   $��16(�6@   $��SemiBold"�6N   6f   �� 5�   "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5�   &   �� $��30��5�   '   $��14(�5�   '   $	��2 0q�5�   '   $	��5 0q�5�   '   $ Số CCCD/CMND��5�   '   ��5�   '   �� *GiaoVien.SoCCCD+$ PropertyChanged=�$ TwoWay=�5�   '   5�   #   5�      �� 5�      $
��10 0q�6)   .+�5�   "   �� $��Nơi CT��6,   $��16(�6:   $��SemiBold"�6H   6`   �� 5�   "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5�   &   ��  �� DisplayMemberPath$
 name��5�   )    �� SelectedValuePath$
 name��5�   )   $��30��5�   )   $	��5 6q�5�   )   $ Nơi CT��5�   )   ��5�   %   �� *
ListCity+ �� SelectedIndex 5�   )   �� *SelectedIndexNoiCT+$ PropertyChanged=�5�   '   5�   #   5�      �� 5�      $
��10 0q�6)   .+�5�   "   �� $!��Hình thức tuyển dụng��6,   $��16(�6H   $��SemiBold"�6V   6n   �� 5�   "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5�   &   �� $��30��5�   %   $	��5 6q�5�   %   $! Hình thức tuyển dụng��5�   %    5�   %   �� *SelectedIndexTD+$ PropertyChanged=�.@�5�   *   �� $��Hợp đồng��67   6L   �� 5�   *   $��Biên chế��67   6L   �� 5�   *   $��Thời vụ��67   6K   5�   '   5�   #   5�      �� 5�      $
��10 0q�6)   .+�5�   "   �� $��Số điện thoại��6,   $��16(�6A   $��SemiBold"�6O   6g   �� 5�   "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5�   &   �� $��30��5�   '   $��14(�5�   '   $	��2 0q�5�   '   $	��5 0q�5�   '   $ Số điện thoại��5�   '   ��5�   )   �� *GiaoVien.Phone+$ PropertyChanged=�$ TwoWay=�5�   '   5�   #   5�      5�      �� 5�      $��1��6%   .+�5�      � $
��10 0q�6(   .+�5�   "   �� .+�5�   &   �� $��Trình độ văn hóa��60   $��16(�6H   $��SemiBold"�6V   6n   �� 5�   &   $ 5��6-   $
��0 10q�6>   $��1 1 1 1q�6L   
�������._�5�   *   �� $	��219��5�   )   $��30��5�   '   $��14(�5�   '   $	��2 0q�5�   '   $	��5 0q�5�   '   $ Trình độ văn hóa��5�   '   ��5�   )   �� *GiaoVien.TrinhDo_VH+$ PropertyChanged=�$ TwoWay=�5�   '   5�   '   5�   #   �� 5�   "   $
��10 0q�6-   .+�5�   &   �� $��Trình độ sư phạm��60   $��16(�6H   $��SemiBold"�6V   6n   �� 5�   &   $ 5��6-   $
��0 10q�6>   $��1 1 1 1q�6L   
�������._�5�   *   �� $	��219��5�   '   $��30��5�   '   $��14(�5�   '   $	��2 0q�5�   '   $	��5 0q�5�   '   $ Trình độ sư phạm��5�   '   ��5�   -   �� *GiaoVien.TrinhDo_SP+$ PropertyChanged=�$ TwoWay=�5�   '   5�   '   5�   #   5�      � 5�      $
��10 0q�6(   .+�5�   "   �� .+�5�   &   �� $��Trình độ chuyên môn��60   $��16(�6K   $��SemiBold"�6Y   6q   �� 5�   &   $ 5��6-   $
��0 10q�6>   $��1 1 1 1q�6L   
�������._�5�   *   �� $	��219��5�   '   $��30��5�   '   $��14(�5�   '   $	��2 0q�5�   '   $	��5 0q�5�   '   $ Trình độ chuyên môn��5�   '   ��5�   -   �� *GiaoVien.TrinhDo_CM+$ PropertyChanged=�$ TwoWay=�5�   '   5�   '   5�   #   �� 5�   "   $
��10 0q�6-   .+�5�   &   �� $%��Ngành chuyên môn đào tạo��60   $��16(�6P   $��SemiBold"�6^   6v   �� 5�   &   $ 5��6-   $
��0 10q�6>   $��1 1 1 1q�6L   
�������._�5�   *   �� $	��219��5�   '   $��30��5�   '   $��14(�5�   '   $	��2 0q�5�   '   $	��5 0q�5�   '   $% Ngành chuyên môn đào tạo��5�   '   ��5�   -   �� *GiaoVien.Nganh_CM+$ PropertyChanged=�$ TwoWay=�5�   '   5�   '   5�   #   5�      �� 5�      $
��10 0q�6)   .+�5�   "   �� $,��&Số giấy chứng nhận giáo viên��6,   $��16(�6P   $��SemiBold"�6^   6v   �� 5�   "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5�   &   �� $ SoGCN��5  )   $ SoGCN��5  )   $��30��5  )   $��14(�5  '   $	��2 0q�5  '   $	��5 0q�5  '   $, &Số giấy chứng nhận giáo viên��5  '   ��5�   )   �� *	ListGCN+ 5   )   �� *SelectedIndexGCN+$ PropertyChanged=�5	  '   5
  #   5     �� 5     $
��10 0q�6)   .+�5  "   �� $&�� Mã sở giao thông vận tải��6,   $��16(�6L   $��SemiBold"�6Z   6r   �� 5  "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5  &   �� $��30��5  '   $��14(�5  '   $	��2 0q�5  '   $	��5 0q�5  '   $&  Mã sở giao thông vận tải��5  '   ��5  )   �� *GiaoVien.MaSoGTVT+$ PropertyChanged=�$ TwoWay=�5  '   5  #   5     �� 5     $
��10 0q�6)   .+�5  "   �� $��Mã cơ sở đào tạo��6,   $��16(�6D   $��SemiBold"�6R   6j   �� 5  "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5  &   �� $��30��5  '   $��14(�5   '   $	��2 0q�5!  '   $	��5 0q�5"  '   $ Mã cơ sở đào tạo��5#  '   ��5  )   �� *GiaoVien.MaCSDT+$ PropertyChanged=�$ TwoWay=�5#  '   5$  #   5%     �� 5'     $
��10 0q�6)   .+�5(  "   �� $��Trạng thái��6,   $��16(�6>   $��SemiBold"�6L   6d   �� 5)  "   $ 5��6)   $
��0 10q�6:   $��1 1 1 1q�6H   
�������._�5*  &   �� $��30��5,  %   $	��5 6q�5-  %   $ Trạng thái��5.  %    5+  %   �� *SelectedIndexStatus+$ PropertyChanged=�$ TwoWay=�.@�5/  *   ��  �� 
IsSelected . $��Họat động��6I   6_   �� 50  *   $��Không hoạt động��67   6S   51  '   52  #   53     � 55     $
��10 0q�6(   .+�56  "   �� $	��219��6-   .+�57  &   �� $��Giáo viên dạy��60   $��0 0 0 15q�6E   $��16(�6W   $��SemiBold"�6e   6}   � 58  &   $��30��60   .+�59  *   ��   MaterialDesignCheckBox#��[ 6�   $��Lý thuyết��6�   $��0 0 10 0q�6�    d� 	IsChecked 63   �� *IsCheckedGV_LT+$ PropertyChanged=�$ TwoWay=�6�   �� 5:  *   #��[ 6�   $��Thực hành��6�    63   �� *IsCheckedGV_TH+$ PropertyChanged=�$ TwoWay=�6�   5;  '   5<  #   �� 5>  "   $	��219��6-   .+�5?  &   �� $��Giới tính��60   $��0 0 0 15q�6A   $��16(�6S   $��SemiBold"�6a   6y   � 5@  &   .+�5A  *   	� $
 TrueY�66     MaterialDesignRadioButton#��[ 6G   $	��Nam��6z   $��0 0 10 0q�6�   6�   	� 5B  *   $
��Nữ��6�    66   �� *IsCheckedGender+$ PropertyChanged=�$ TwoWay=�6�   5C  '   5D  #   5E     5F     5G     � 5H     $��1��6   .+�5I     ��  �� Orientation$ 
Horizontal=�6%   $
��Left=�6>   $��10 0 0 0q�6Y   .+�5J     �� #��[ 5K  !   $	��100��5L  !   $��0 0 10 0q�5M  !   $( "Xóa giáo viên khỏi danh sách��5N  !   $
��Xóa��5O  !   $��0q�5P  !   ���#B31312���#ffffff��5S  !   �� *RemoveTeacher+��5T  !   �� $	 editTeacherWindow��5U     5V     �� 5W     $ 
Horizontal=�6%   $��Right=�6>   $��0 0 10 0q�6Z   .+�5X     �� #��[ 5Y  %   $	��100��5Z  %   $/ )Resource name: MaterialDesignRaisedButton��5]  %   $��Hủy��5^  %   $��0q�5_  %   
�������$��0 0 10 0q�5a  %   ���#ffffff��5[  %   �� *closeInsertTeacherWindow+��5\  %   �� $	 editTeacherWindow��5c     �� 5e     #��[ 5g  %   $	��100��5h  %   $/ )Resource name: MaterialDesignRaisedButton��5k  %   $��Duyệt��5l  %   $��0q�5m  %   ���	#FF6FD1E8$��0 0 10 0q�5o  %   ���#ffffff{�5f  %   �� *
IsEnable+��5i  %   �� *AgreeTeacherWindow+��5j  %   �� $	 editTeacherWindow��5q     �� 5u     #��[ 5v  %   $	��130��5w  %   $/ )Resource name: MaterialDesignRaisedButton��5x  %   $��Cập nhật��5y  %   $��0q�5z  %   ���#08bf96���#ffffff��5}  %   �� $	 editTeacherWindow����5~  %   �� *editTeacher+5     5�     5�     5�     5�     5�     5�     5�     