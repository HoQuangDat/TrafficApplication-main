   M S B A M L   `   `   `  ����D  @giaothong, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:giaothong	giaothong  5   	       giaothong.TeacherWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:giaothong     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   -   5        $��teacherWindow��5   	   $��Regular"�5
   	      MaterialDesignFont#���   5   	     	teachervm  � DataContext#  [ 5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency . 
������  � Title$ Teacher Window��5   	   $	��650��6    $
��1180��6-   .��5      ��  �� CornerRadius$ 10��6   ���#ffffff._�5   
   �  � RowDefinitions 5      �� 5      $��40 �6    6.   �� 5      $��30 �6    6.   �� 5      $��50 �6    6.   �� 5      6"   �� 5      $��40 �6    6.   .+�5       �� $
��Left=�6   $��0q�60   
������ $��30��6]   $��50��6i   ��6t   �� *closeTeacherWindow+��6�   �� 	 �� ElementName$	 teacherWindow��.��5!      '  !MaterialDesignThemes.Wpf.PackIcon  $��20��6*   $��20��65   ���#08bf96
   Kind$
 Logout=�6V   5"      � 5$       � ColumnDefinitions 5%      �� 5&      $��* �6'   �� 5'      $
��Auto �6'   �� 5(      $
��Auto �6'   .+�5*        -   5        $��1��5*   *   $
 WindowMinimize=�6:   $��	0 10 10 0q�6P    � Cursor$
 Hand��6�     5+      -   5        $��2��5+   *   $
 WindowMaximize=�6:   $��	0 10 10 0q�6P   $
 Hand��6�   5,      �� 5.      $��1��6   ���#08bf96$��22(�6:   $��Center=�6H   $��Center=�6c   $��DANH SÁCH GIÁO VIÊN��6�   $��SemiBold"�6�   6�   � 50      $��2��6    51      �� 52      6)   �� 53      6)   .+�56      � $��10 0 0 0q�6   $��Center=�6.   .+�57      �� $	��100��58      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Trạng thái��59       & "MaterialDesignFloatingHintComboBox#��[ 5;       �� SelectedIndex 5:      �� *SelectedIndex+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�.  (System.Windows.Interactivity.Interaction   Triggers 5<      /  )System.Windows.Interactivity.EventTrigger  5=         	EventName$ SelectionChanged��6-   .  (System.Windows.Interactivity.TriggerBase   Actions. 5>   "   6  0System.Windows.Interactivity.InvokeCommandAction     Command 68   �� *selectionChanged+6_   5?      .@�5A      �� $��
Tất cả��6'   6:   �� 5B      $��Hợp đồng��6'   6<   �� 5C      $��Biên chế��6'   6<   �� 5D      $��Thời vụ��6'   6;   �� 5E      $��Dạy lý thuyết��6'   6A   �� 5F      $��Dạy thực hành��6'   6A   5G      �� 5I      $��10 0 0 0q�5J      $	��220��5K      $2 ,Tìm kiếm theo Mã, CCCD hoặc Tên GV...��5M       % !MaterialDesignFloatingHintTextBox#��[ 5N      ��5L      �� *SearchTeacher+$ PropertyChanged=�$ TwoWay=�.��5N   V    5O        5P      $ TextChanged��6-   . 5Q   "      68   �� *textChanged+6Z   5R      .��5S   2   5T      5U      � 5W      $��0 0 10 0q�6   $��1��6.   $��Right=�6>   $��Center=�6Z   .+�5X      �� {�.   MaterialDesignRaisedButton#��[ 5[      $	��100��5\      $��0 0 10 0q�5]       � ToolTip$/ )Resource name: MaterialDesignRaisedButton��5^      $��	Báo cáo��5_      ���	#FFB4B4B4$��0q�5a      ���#ffffff��5Z      �� *ExportExcel+5c      �� 5e      #��[ 5f      $	��145��5g      $/ )Resource name: MaterialDesignRaisedButton��5h      $��Giấy chứng nhận��5i      $��0 0 10 0q�5j      $��0q�5k      ���#08bf96���#ffffff��5n      �� *	viewGCN+��5o      �� $	 teacherWindow��5p      �� 5r      #��[ 5s      $	��130��5t      $/ )Resource name: MaterialDesignRaisedButton��5u      $��Thêm giáo viên��5v      $��0q�5w      ���#08bf96���#ffffff��5z      �� *viewInsertTeacher+��5{      �� $	 teacherWindow��5|      5}      5~      � 5�      $��3��6   $��	10 0 10 0q�6     5�      �� 5�      $	��86* �6'   �� 5�      $	��59* �6'   .+�5�      �� $
��auto��6   $��Visible=�6�   $��Disabled=�6�   $��2��6  ��6)   �� *ListTeacher+ �� SelectedItem 6M   �� *SelectedItem+$ PropertyChanged=� �� View 5�      �� 5�      .P�5�      �� ��5�   "   �� 5�   &   $��AnhCD��60   $
��Left=�6=   6Z    �� CellTemplate 5�   "   �� 5�   &   .R�5�   *   �� $��25 0 0 0q�60   $��Center=�6B   $��Center=�6�   $��20��6�   $��20��6�   ��6_   �� *AnhCD+$ PropertyChanged=�6�   5�   '   5�      �� 5�      $
��MaGV��6S    �� DisplayMemberBinding 6-   �� *MaGV+6S   �� 5�      $��HoDem��6T    6-   �� *HoDem+6T   �� 5�      $��TenGV��6T    6-   �� *TenGV+6e   �� 5�      $��NgaySinh��6W    6-   �� *
NgaySinh+6W   �� 5�      $��SoCCCD��6U    6-   �� *SoCCCD+6U   �� 5�      $��NoiCT��6T    6-   �� *NoiCT+6T   �� 5�      $��GioiTinh��6W    6-   �� *
GioiTinh+6W   �� 5�      $��Phone��6T    6-   �� *Phone+6T   �� 5�      $��	TuyenDung��6X    6-   �� *	TuyenDung+6X   �� 5�      $��
TrinhDo_VH��6Y    6-   �� *
TrinhDo_VH+6Y   �� 5�      $��
TrinhDo_CM��6Y    6-   �� *
TrinhDo_CM+6Y   �� 5�      $��Nganh_CM��6W    6-   �� *
Nganh_CM+6W   �� 5�      $��
TrinhDo_SP��6Y    6-   �� *
TrinhDo_SP+6Y   �� 5�      $��GV_LT��6T    6-   �� *GV_LT+6T   �� 5�      $��GV_TH��6T    6-   �� *GV_TH+6T   �� 5�      $��SoGCN��6T    6-   �� *SoGCN+6T   �� 5�      $��MaSoGTVT��6W    6-   �� *
MaSoGTVT+6W   �� 5�      $��MaCSDT��6U    6-   �� *MaCSDT+6U   �� 5�      $��	TrangThai��6X    6-   �� *	TrangThai+6X   �� 5�      $��NguoiCapNhat��6[    6-   �� *NguoiCapNhat+6[   �� 5�      $��NgayCapNhat��6Z    6-   �� *NgayCapNhat+6Z   �� 5�      $��
NguoiDuyet��6Y    6-   �� *
NguoiDuyet+6Y   �� 5�      $��	NgayDuyet��6X    6-   �� *	NgayDuyet+6X   5�       5�        5�      $ PreviewMouseLeftButtonUp��6)   . 5�           CommandParameter 64   �� $	 teacherWindow�� 6k   �� *previewMouseLeftButtonUp+6�   5�      5�      5�      � 5�      $��4��6   $��	10 0 10 0q�6    .+�5�      � .+�5�      �� $
��Left=�6    $��16(�6;   $��Trang��6I   $��Center=�6V   
�������6�   �� 5�      $
��Left=�6    $��16(�6;   $��5 0 0 0q�6�   $��Center=�6�   
���������6I   �� *CurrentPage+$ PropertyChanged=�6�   �� 5�      $
��Left=�6    $��16(�6;   $��/��6I   $��Center=�6R   
�������6�   �� 5�      $
��Left=�6    $��16(�6;   $��Center=�6�   
���������6I   �� *
TotalPages+$ PropertyChanged=�6�   5�      � 5�      $��Right=�6   $��Center=�68   .+�5�      �� $��Right=�6>   $��0q�6Z   
������ $��30��6�   $��40��6�   ��6   �� *previousPage+.��5�        $��Center=�62   $��30��6O   $��25��6Z   ���#08bf96$
 ChevronLeft=�6|   5�      �� 5�      $��Right=�6:   $��0q�6V   
������ $��30��6�   $��40��6�   ��6   �� *
nextPage+.��5�        $��Center=�62   $��30��6O   $��25��6Z   ���#08bf96$
 ChevronRight=�6}   5�      5�      5�      5�      5�      5�      