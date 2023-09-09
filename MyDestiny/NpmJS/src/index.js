import LeaderLine from 'leader-line';

function createLeaderLine(elementAId, elementBId) {
    console.log("createLeaderLine");
    const line = new LeaderLine(document.getElementById(elementAId), document.getElementById(elementBId));
    return line;
}

function sum(a, b) {
    console.log("sum", a, b);
    return a + b;
}

export { createLeaderLine, sum };