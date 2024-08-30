import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class MusicSheetApp {

    public static void main(String[] args) {
      
        JFrame frame = new JFrame("Music Sheet Creator");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setSize(800, 600);
        
 
        JMenuBar menuBar = new JMenuBar();
        

        JMenu fileMenu = new JMenu("File");
        JMenu editMenu = new JMenu("Edit");
        JMenu sourceMenu = new JMenu("Source");
        JMenu runMenu = new JMenu("Run");
        JMenu helpMenu = new JMenu("Help");
        
 
        menuBar.add(fileMenu);
        menuBar.add(editMenu);
        menuBar.add(sourceMenu);
        menuBar.add(runMenu);
        menuBar.add(helpMenu);
        
    
        frame.setJMenuBar(menuBar);

       
        JToolBar toolBar = new JToolBar();
        toolBar.setFloatable(false); // Fixed toolbar

        
        JButton recordButton = new JButton("Record");
        recordButton.setBackground(Color.RED); 
        recordButton.setForeground(Color.WHITE); 
        recordButton.setOpaque(true); 
        recordButton.setBorderPainted(false); 
      
        
        toolBar.add(Box.createHorizontalGlue()); 
        toolBar.add(recordButton);
        toolBar.add(Box.createHorizontalGlue());

       
        JPanel sheetPanel = new JPanel() {
            @Override
            protected void paintComponent(Graphics g) {
                super.paintComponent(g);
                drawMusicSheet(g); 
            }
        };
        sheetPanel.setPreferredSize(new Dimension(800, 200)); 
        sheetPanel.setBorder(BorderFactory.createLineBorder(Color.BLACK)); 
 
        frame.setLayout(new BorderLayout());
        frame.add(toolBar, BorderLayout.CENTER); 
        frame.add(sheetPanel, BorderLayout.SOUTH); 

    
        frame.setVisible(true);
    }

  
    public static void drawMusicSheet(Graphics g) {
        g.setColor(Color.BLACK);
        int lineHeight = 20; 
        int startY = 50;

      
        for (int i = 0; i < 5; i++) {
            g.drawLine(50, startY + (i * lineHeight), 750, startY + (i * lineHeight));
        }

        
    }
}