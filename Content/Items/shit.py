import shutil
import os

def duplicate_file_with_prefixes(file_path, prefix_list):
    """
    Duplicates a file multiple times with different prefixes added to the filename.
    
    Args:
        file_path (str): Path to the original file
        prefix_list (list): List of prefixes to add to the filename
    
    Returns:
        list: Paths of the newly created files
    """
    if not os.path.exists(file_path):
        raise FileNotFoundError(f"File not found: {file_path}")
    
    # Get directory, filename, and extension
    directory = os.path.dirname(file_path)
    filename = os.path.basename(file_path)
    name, extension = os.path.splitext(filename)
    
    created_files = []
    
    for prefix in prefix_list:
        # Create new filename with prefix
        new_filename = f"{prefix}{name}{extension}"
        new_file_path = os.path.join(directory, new_filename)
        
        # Copy the file
        shutil.copy2(file_path, new_file_path)
        created_files.append(new_file_path)
        print(f"Created: {new_file_path}")
    
    return created_files

# Example usage:
if __name__ == "__main__":
    # Your file and prefix list
    original_file = "ModularGun.png"
    prefixes = ["Copper", "Iron", "Tin", "Lead", "Silver", "Tungsten", "Gold", "Platinum", "Palladium", "Cobolt", "Adamantite", "Titanium", "Hallowed", "Chlorophyte", "Luminite"]
    
    try:
        new_files = duplicate_file_with_prefixes(original_file, prefixes)
        print(f"\nSuccessfully created {len(new_files)} files")
    except FileNotFoundError as e:
        print(f"Error: {e}")