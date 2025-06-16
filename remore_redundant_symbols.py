import os

# Specify the folder path here
folder_path = r'C:\Users\olmit\solitaire-style\Assets\Asset_PlayingCards\Prefabs\Deck02'  # Change this to your folder path

# Loop through all files in the directory
for filename in os.listdir(folder_path):
    if "Deck02_" in filename:
        new_name = filename.replace("Deck02_", "")
        old_path = os.path.join(folder_path, filename)
        new_path = os.path.join(folder_path, new_name)
        os.rename(old_path, new_path)
        print(f'Renamed: {filename} -> {new_name}')
