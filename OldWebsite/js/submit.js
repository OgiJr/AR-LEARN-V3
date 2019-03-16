// Add more models dynamically

var i = 0; // number of models

function fixi() {
	var input = document.getElementById("i");
	input.value = i;
}

function add() {
	var modelHtml = `
	<tr id="model_${i}">
		<td>
			<input class="textfield" type="text" name="m_name${i}" form="form" maxlength="255" required>
		</td>
		<td>
			<input class="textfield" type="text" name="m_desc${i}" form="form" maxlength="255" required>
		</td>
		<td>
			<input type="file" class="filebutton" name="m_pic${i}" form="form" accept="image/png, image/jpeg" required>
		</td>
		<td>
			<input type="file" class="filebutton" name="m_model${i}" form="form" accept=".fbx, .prefab" required>
		</td>
		<td>
			<input type="file" class="filebutton" name="m_info${i}" form="form" accept="text/markdown" required>
		</td>
	</tr>
`;
	var table = document.getElementById("add-here");
	table.innerHTML += modelHtml;
	i++;
	fixi();
}

function remove() {
	i--;
	var element = document.getElementById(`model_${i}`);
	element.parentNode.removeChild(element);
	fixi();
}