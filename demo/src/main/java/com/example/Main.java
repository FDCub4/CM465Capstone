import javax.swing.*;
import javax.swing.undo.UndoManager;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import javax.swing.Timer;
import java.util.TimerTask;

public class MusicSheetApp {

    private static BufferedImage backgroundImage;

    public static void main(String[] args) {
        // custom background
        try {
            
            backgroundImage = ImageIO.read(new File("C:\\Users\\awazn\\Downloads\\music-player-2951399_1280.jpg\""));
        } catch (Exception e) {
            e.printStackTrace();
            backgroundImage = null; 
        }

        //  main frame
        JFrame frame = new JFrame("Music Sheet Creator");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setSize(800, 600);
        
        //menu bar
        JMenuBar menuBar = new JMenuBar();
        
        // menu items
        JMenu fileMenu = new JMenu("File");
        JMenuItem newItem = new JMenuItem("New");
        JMenuItem openItem = new JMenuItem("Open");
        JMenuItem saveItem = new JMenuItem("Save");
        JMenuItem saveAsItem = new JMenuItem("Save As");
        JMenuItem importItem = new JMenuItem("Import");
        JMenuItem exportItem = new JMenuItem("Export");

        //actions to menu items
        newItem.addActionListener(e -> JOptionPane.showMessageDialog(frame, "New Music Sheet Created."));
        openItem.addActionListener(e -> {
            JFileChooser fileChooser = new JFileChooser();
            int returnValue = fileChooser.showOpenDialog(frame);
            if (returnValue == JFileChooser.APPROVE_OPTION) {
                File selectedFile = fileChooser.getSelectedFile();
                JOptionPane.showMessageDialog(frame, "Opening: " + selectedFile.getName());
            }
        });
        saveItem.addActionListener(e -> {
            JFileChooser fileChooser = new JFileChooser();
            int returnValue = fileChooser.showSaveDialog(frame);
            if (returnValue == JFileChooser.APPROVE_OPTION) {
                File fileToSave = fileChooser.getSelectedFile();
                JOptionPane.showMessageDialog(frame, "Saving: " + fileToSave.getName());
            }
        });
        saveAsItem.addActionListener(e -> {
            JFileChooser fileChooser = new JFileChooser();
            int returnValue = fileChooser.showSaveDialog(frame);
            if (returnValue == JFileChooser.APPROVE_OPTION) {
                File fileToSave = fileChooser.getSelectedFile();
                JOptionPane.showMessageDialog(frame, "Saving As: " + fileToSave.getName());
            }
        });
        importItem.addActionListener(e -> {
            JFileChooser fileChooser = new JFileChooser();
            int returnValue = fileChooser.showOpenDialog(frame);
            if (returnValue == JFileChooser.APPROVE_OPTION) {
                File selectedFile = fileChooser.getSelectedFile();
                JOptionPane.showMessageDialog(frame, "Importing: " + selectedFile.getName());
            }
        });
        exportItem.addActionListener(e -> {
            JFileChooser fileChooser = new JFileChooser();
            int returnValue = fileChooser.showSaveDialog(frame);
            if (returnValue == JFileChooser.APPROVE_OPTION) {
                File fileToExport = fileChooser.getSelectedFile();
                JOptionPane.showMessageDialog(frame, "Exporting: " + fileToExport.getName());
            }
        });

        fileMenu.add(newItem);
        fileMenu.add(openItem);
        fileMenu.add(saveItem);
        fileMenu.add(saveAsItem);
        fileMenu.add(importItem);
        fileMenu.add(exportItem);
        menuBar.add(fileMenu);

        // Create the Edit menu
        JMenu editMenu = new JMenu("Edit");
        UndoManager undoManager = new UndoManager();
        JTextArea textArea = new JTextArea();
        textArea.getDocument().addUndoableEditListener(undoManager);
        JMenuItem undoItem = new JMenuItem("Undo");
        JMenuItem cutItem = new JMenuItem("Cut");
        JMenuItem copyItem = new JMenuItem("Copy");
        JMenuItem pasteItem = new JMenuItem("Paste");
        JMenuItem deleteItem = new JMenuItem("Delete");
        JMenuItem selectAllItem = new JMenuItem("Select All");

        undoItem.addActionListener(e -> {
            if (undoManager.canUndo()) {
                undoManager.undo();
            }
        });
        cutItem.addActionListener(e -> textArea.cut());
        copyItem.addActionListener(e -> textArea.copy());
        pasteItem.addActionListener(e -> textArea.paste());
        deleteItem.addActionListener(e -> textArea.replaceSelection(""));
        selectAllItem.addActionListener(e -> textArea.selectAll());

        editMenu.add(undoItem);
        editMenu.add(cutItem);
        editMenu.add(copyItem);
        editMenu.add(pasteItem);
        editMenu.add(deleteItem);
        editMenu.add(selectAllItem);
        menuBar.add(editMenu);

        //Source menu
        JMenu sourceMenu = new JMenu("Source");
        JMenuItem addLibraryItem = new JMenuItem("Add Library");
        JMenuItem viewSourceItem = new JMenuItem("View Source Code");
        JMenuItem preferencesItem = new JMenuItem("Edit Preferences");

        addLibraryItem.addActionListener(e -> JOptionPane.showMessageDialog(frame, "Add Library functionality triggered."));
        viewSourceItem.addActionListener(e -> JOptionPane.showMessageDialog(frame, "View Source Code functionality triggered."));
        preferencesItem.addActionListener(e -> JOptionPane.showMessageDialog(frame, "Edit Preferences functionality triggered."));

        sourceMenu.add(addLibraryItem);
        sourceMenu.add(viewSourceItem);
        sourceMenu.add(preferencesItem);
        menuBar.add(sourceMenu);

        //Run menu
        JMenu runMenu = new JMenu("Run");
        JMenuItem compileItem = new JMenuItem("Compile/Validate Sheet");
        JMenuItem playItem = new JMenuItem("Play Sheet");
        JMenuItem stopItem = new JMenuItem("Stop Playback");

        compileItem.addActionListener(e -> JOptionPane.showMessageDialog(frame, "Compile/Validate functionality triggered."));
        playItem.addActionListener(e -> JOptionPane.showMessageDialog(frame, "Play Sheet functionality triggered."));
        stopItem.addActionListener(e -> JOptionPane.showMessageDialog(frame, "Stop Playback functionality triggered."));

        runMenu.add(compileItem);
        runMenu.add(playItem);
        runMenu.add(stopItem);
        menuBar.add(runMenu);

        //Help menu
        JMenu helpMenu = new JMenu("Help");
        JMenuItem viewHelpItem = new JMenuItem("View Help Documentation");
        JMenuItem aboutItem = new JMenuItem("About");
        JMenuItem reportIssueItem = new JMenuItem("Report Issue");

        viewHelpItem.addActionListener(e -> JOptionPane.showMessageDialog(frame, "Help Documentation triggered."));
        aboutItem.addActionListener(e -> JOptionPane.showMessageDialog(frame, "About: Music Sheet Creator\nVersion 1.0\nDeveloped for Capstone-465-A."));
        reportIssueItem.addActionListener(e -> JOptionPane.showMessageDialog(frame, "Report Issue functionality triggered."));

        helpMenu.add(viewHelpItem);
        helpMenu.add(aboutItem);
        helpMenu.add(reportIssueItem);
        menuBar.add(helpMenu);
        frame.setJMenuBar(menuBar);

        //toolbar
        JToolBar toolBar = new JToolBar();
        toolBar.setFloatable(false); // Fixed toolbar

        JButton recordButton = new JButton("Record");
        recordButton.setBackground(Color.RED);  
        recordButton.setForeground(Color.BLACK);  
        recordButton.setOpaque(true);  
        recordButton.setBorderPainted(false);  

      
        JLabel statusLabel = new JLabel(" ");
        statusLabel.setHorizontalAlignment(SwingConstants.CENTER);
        statusLabel.setPreferredSize(new Dimension(200, 30)); 
        recordButton.addActionListener(new ActionListener() {
            private boolean isRecording = false; 
            private Timer statusTimer; // Timer to change status message

            @Override
            public void actionPerformed(ActionEvent e) {
                if (isRecording) {
                    // Stop the recording
                    recordButton.setText("Record");
                    recordButton.setBackground(Color.GREEN); 
                    statusLabel.setText(" "); 
                    if (statusTimer != null) {
                        statusTimer.stop(); 
                    }
                } else {
                    //Recording
                    recordButton.setText("Stop");
                    recordButton.setBackground(Color.RED); //background to indicate recording

                    //Message
                    String[] messages = {"Please wait...", "Listening...", "Hmmm...", "Got it...", "One moment..."};
                    TimerTask task = new TimerTask() {
                        private int index = 0;

                        @Override
                        public void run() {
                            SwingUtilities.invokeLater(() -> {
                                statusLabel.setText(messages[index]);
                                index = (index + 1) % messages.length;
                            });
                        }
                    };

                    //timer start
                    statusTimer = new Timer(3000, e1 -> task.run());
                    statusTimer.start();
                }
                isRecording = !isRecording;
            }
        });

        //button and label for the toolbar
        toolBar.add(Box.createHorizontalGlue()); 
        toolBar.add(recordButton);
        toolBar.add(Box.createHorizontalStrut(10));
        toolBar.add(statusLabel);
        toolBar.add(Box.createHorizontalGlue()); 

        //panel where the music sheet will be drawn
        JPanel sheetPanel = new JPanel() {
            @Override
            protected void paintComponent(Graphics g) {
                super.paintComponent(g);
                drawMusicSheet(g); 
            }
        };
        sheetPanel.setPreferredSize(new Dimension(1000, 500)); 
        sheetPanel.setBorder(BorderFactory.createLineBorder(Color.BLACK));

        // Layout
        frame.setLayout(new BorderLayout());
        frame.add(toolBar, BorderLayout.NORTH);
        frame.add(sheetPanel, BorderLayout.CENTER); 
        frame.add(new JScrollPane(textArea), BorderLayout.CENTER);

        frame.setVisible(true);
    }

    // Method to draw music sheet lines
    private static void drawMusicSheet(Graphics g) {
        Graphics2D g2d = (Graphics2D) g;
        g2d.setColor(Color.BLACK); 

        int margin = 1000;
        int lineSpacing = 20;
        int numberOfLines = 10; // Number of lines on the music sheet

        // Draw the music sheet lines
        for (int i = 0; i < numberOfLines; i++) {
            int y = margin + i * lineSpacing;
            g2d.drawLine(margin, y, 750, y); 
        }
    }
}
